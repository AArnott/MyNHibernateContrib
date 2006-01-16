using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib
{
	public interface IDbMgr
	{
		void Save(object o);
		void SaveNew(object o);
		void Delete(object o);
		void ClearDatabase();

		T Get<T>(int id);
		object Get(Type type, int id);
		IList<T> GetList<T>();
		System.Collections.IList GetList(Type type);
	}
}
