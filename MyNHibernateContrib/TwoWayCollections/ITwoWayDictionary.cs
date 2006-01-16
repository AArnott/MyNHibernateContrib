using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib.TwoWayCollections
{
	public interface ITwoWayDictionary<TKey, TValue, PARENT> : IDictionary<TKey, TValue>
	{
		PARENT Parent { get; set; }
	}
}
