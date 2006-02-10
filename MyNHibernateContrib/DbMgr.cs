using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Web;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using MyNHibernateContrib.GenericCollections;

namespace MyNHibernateContrib
{
	public class DbMgr : IDbMgr, IHttpModule, IDisposable
	{
		public delegate void InitializeConfigurationEventHandler(Configuration config);
		public static event InitializeConfigurationEventHandler InitializeConfiguration;
		protected static void OnInitializeConfiguration()
		{
			InitializeConfigurationEventHandler initializeConfiguration = InitializeConfiguration;
			if (initializeConfiguration == null)
				throw new InvalidOperationException("InitializeConfiguration event must be handled first.");
			initializeConfiguration(config);
		}

		public delegate void InitializeDataEventHandler();
		public static event InitializeDataEventHandler InitializeData;
		protected static void OnInitializeData()
		{
			InitializeDataEventHandler initializeData = InitializeData;
			if (initializeData != null)
				initializeData();
		}

		public delegate void OpeningSessionEventHandler();
		public static event OpeningSessionEventHandler OpeningSession;
		protected static void OnOpeningSession()
		{
			OpeningSessionEventHandler openingSession = OpeningSession;
			if (openingSession != null)
				openingSession();
		}

		private static Configuration config;
		private static Configuration Config
		{
			get
			{
				if (config == null)
				{
					config = new Configuration();
					OnInitializeConfiguration();
				}
				return config;
			}
		}
		private static ISessionFactory factory;
		protected static ISessionFactory Factory
		{
			get
			{
				lock (typeof(DbMgr))
					if (factory == null)
						factory = Config.BuildSessionFactory();
				return factory;
			}
		}

		private const string NHibernateDbMgrKey = "NHDbMgr";
		// Only used when not running in HttpModule mode.
		private static ISession m_session;

		public static bool UseTransactions = true;

		#region Logging Tools
		protected log4net.ILog log
		{
			get { return log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); }
		}
		protected static string GetHttpRequestClues()
		{
			if (HttpContext.Current == null) return string.Empty;
			return string.Format(" ({0}: {1})",
				HttpContext.Current.Request.UserHostAddress,
				HttpContext.Current.Request.RawUrl);
		}
		#endregion

		private static bool recentlyConstructed;

		public static bool RecentlyConstructed
		{
			get { return recentlyConstructed; }
			private set { recentlyConstructed = value; }
		}

		private static bool isConstructingDatabase = false;
		public static void ConstructDatabase()
		{
			if (isConstructingDatabase) return; // avoid loops when this is called as a result of opening a session
			isConstructingDatabase = true;
			try
			{
				// Clear the database before destroying it (if it exists)
				// so that the second-level cache (if any) is cleared also.
				// See issue http://jira.nhibernate.org/browse/NH-342
				ClearDatabase();
				EndSession();
			}
			catch { } // ignore errors, as it's probably just that the database didn't exist before.
			try
			{
				new SchemaExport(Config).Create(true, true);
				OnInitializeData();
				RecentlyConstructed = true;
			}
			finally
			{
				isConstructingDatabase = false;
			}
		}

		public static void DestructDatabase()
		{
			// Clear the database before destroying it
			// so that the second-level cache (if any) is cleared also.
			// See issue http://jira.nhibernate.org/browse/NH-342
			ClearDatabase();
			EndSession();
			new SchemaExport(Config).Drop(true, true);
			RecentlyConstructed = false;
		}

		#region Session managers
		private static ISession session
		{
			get
			{
				HttpContext context = HttpContext.Current;
				return (context != null) ? context.Items[NHibernateDbMgrKey] as ISession : m_session;
			}
			set
			{
				HttpContext context = HttpContext.Current;
				if (context != null)
					context.Items[NHibernateDbMgrKey] = value;
				else
					m_session = value;
			}
		}
		public static ISession Session
		{
			get
			{
				lock (typeof(DbMgr))
				{
					ISession session = DbMgr.session;
					if (session == null)
						session = OpenSession();
					return session;
				}
			}
		}
		public static bool IsSessionActive
		{
			get { return session != null; }
		}
		private static ISession OpenSession()
		{
			ISession session = DbMgr.session;
			if (session != null) return session; // session already open
			OnOpeningSession(); // notify others that we're opening a session
			if ((session = DbMgr.session) != null) return session; // the event MIGHT have opened the session
			session = Factory.OpenSession();
			if (UseTransactions)
				session.BeginTransaction();
			return DbMgr.session = session;
		}
		public static void EndSession()
		{
			lock (typeof(DbMgr))
			{
				ISession session = DbMgr.session;
				if (session == null) return;
				if (session.Transaction != null &&
					(!session.Transaction.WasRolledBack || session.Transaction.WasCommitted))
					session.Transaction.Commit();
				session.Close();
				session.Dispose();
				DbMgr.session = null;
			}
		}
		/// <summary>
		/// Ends the current session, saving all data, and begins a new one.
		/// </summary>
		/// <remarks>
		/// Ends the current session and begins a new one,
		/// effectively clearing the cache.
		/// </remarks>
		public static void RestartSession()
		{
			EndSession();
			// Don't start a new one, in case a new one isn't needed.
			// The new one will start automatically if needed.
		}

		public static void Rollback()
		{
			lock (typeof(DbMgr))
			{
				ISession session = DbMgr.session;
				if (session == null)
					throw new InvalidOperationException("No active session to rollback changes for.");
				if (session.Transaction == null)
					throw new InvalidOperationException("Session must have an associated transaction to support rollbacks.");

				session.Transaction.Rollback();
				// I think the NHibernate docs say to redo the session
				// when rolling back a transaction.
				EndSession();
				// Don't start a new one, in case a new one isn't needed.
				// The new one will start automatically if needed.
			}
		}

		public static void Flush()
		{
			ISession session = DbMgr.session;
			if (session != null)
				session.Flush();
		}

		public static void Commit()
		{
			lock (typeof(DbMgr))
			{
				ISession session = DbMgr.session;
				if (session == null) return;
				session.Flush();
				if (session.Transaction == null) return;
				session.Transaction.Commit();
				if (UseTransactions)
					session.BeginTransaction();
			}
		}
		#endregion

		public static void Save(object o)
		{
			Session.SaveOrUpdate(o);
		}
		void IDbMgr.Save(object o)
		{
			DbMgr.Save(o);
		}

		public static void SaveNew(object o)
		{
			Session.Save(o);
		}
		void IDbMgr.SaveNew(object o)
		{
			DbMgr.SaveNew(o);
		}

		public static void Delete(object o)
		{
			Session.Delete(o);
		}
		void IDbMgr.Delete(object o)
		{
			DbMgr.Delete(o);
		}

		public static void ClearDatabase()
		{
			Session.Delete("from System.Object");
		}
		void IDbMgr.ClearDatabase() { DbMgr.ClearDatabase(); }

		public static T Get<T>(int id)
		{
			return (T)Session.Get(typeof(T), id);
		}
		T IDbMgr.Get<T>(int id) { return DbMgr.Get<T>(id); }

		public object Get(Type type, int id)
		{
			return Session.Get(type, id);
		}

		public static IList<T> GetList<T>()
		{
			return new IListWrapper<T>(Session.CreateCriteria(typeof(T)).List());
		}
		public static IList<T> GetList<T>(NHibernate.Expression.Order sortOrder, int firstRow, int maxResults)
		{
			ICriteria criteria = Session.CreateCriteria(typeof(T));
			if (sortOrder != null) criteria.AddOrder(sortOrder);
			if (firstRow > -1) criteria.SetFirstResult(firstRow);
			if (maxResults > -1) criteria.SetMaxResults(maxResults);
			return new IListWrapper<T>(criteria.List());
		}
		IList<T> IDbMgr.GetList<T>() { return DbMgr.GetList<T>(); }

		public System.Collections.IList GetList(Type type)
		{
			return Session.CreateCriteria(type).List();
		}

		#region IHttpModule Members

		public void Dispose()
		{
			if (factory != null)
				factory.Close();
		}

		void IHttpModule.Init(HttpApplication context)
		{
			context.EndRequest += new EventHandler(HttpApplication_EndRequest);
			context.Error += new EventHandler(HttpApplication_Error);
		}

		void HttpApplication_Error(object sender, EventArgs e)
		{
			ISession session = DbMgr.session;
			if (session != null)
			{
				if (session.Transaction != null)
				{
					Rollback();
					log.Info("Error in web application caused an ISession rollback.");
				}
				else
				{
					log.Warn("Unable to rollback changes after web application error because no transaction is set up.");
				}
			}
		}

		void HttpApplication_EndRequest(object sender, EventArgs e)
		{
			HttpApplication application = (HttpApplication)sender;
			HttpContext context = application.Context;

			if (session != null)
			{
				try
				{
					EndSession();
				}
				catch (Exception ex)
				{
					log.Error("Error in closing NHibernate session.", ex);
					throw;
				}
			}

			context.Items.Remove(NHibernateDbMgrKey);
		}

		#endregion
	}
}
