using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib.TwoWayCollections
{
	public interface IReferenceableParentDictionary<TKey, TValue, PARENT>
		where TValue : class
		where PARENT : class
	{
		ITwoWayDictionary<TKey, TValue, PARENT> ContainingDictionary { get; }
	}
}
