using System;

using NHibernate;
using NHibernate.Type;
using NHibernate.SqlTypes;



namespace MyNHibernateContrib.Nullables
{
	/// <summary>
	/// Abstract type used for implementing types from the Nullables library.
	/// </summary>
	public abstract class NullableTypesType : NullableType
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger( typeof(NullableTypesType) ); 		

		public NullableTypesType(SqlType type) : base(type)
		{
		}

		public override object NullSafeGet(System.Data.IDataReader rs, string name)
		{
			int index = rs.GetOrdinal(name);

			if( rs.IsDBNull(index) ) 
			{
				if ( log.IsDebugEnabled ) 
				{
					log.Debug("returning null as column: " + name);
				}

				return NullValue; //this value is determined by the subclass.
			}
			else 
			{		
				object val = null;
				try 
				{
					val = Get(rs, index);
				}
				catch(System.InvalidCastException ice) 
				{
					throw new ADOException(
						"Could not cast the value in field " + name + " to the Type " + this.GetType().Name + 
						".  Please check to make sure that the mapping is correct and that your DataProvider supports this Data Type.", ice);
				}

				if ( log.IsDebugEnabled ) 
				{
					log.Debug("returning '" + ToString(val) + "' as column: " + name);
				}

				return val;
			}
		}

		public override object Get(System.Data.IDataReader rs, string name)
		{
			return Get(rs, rs.GetOrdinal(name));
		}

		public abstract object NullValue{ get; }
	}
}
