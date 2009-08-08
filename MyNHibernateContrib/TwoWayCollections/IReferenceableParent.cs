using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib.TwoWayCollections
{
	public interface IReferenceableParent<CHILD, PARENT>
		where CHILD : class
		where PARENT : class
	{
		ITwoWayCollection<CHILD, PARENT> ContainingCollection { get; }
	}
}
