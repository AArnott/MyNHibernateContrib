using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib.TwoWayCollections
{
	public interface IParentReferencingNode<CHILD, PARENT>
		where CHILD : class
		where PARENT : class
	{
		PARENT Parent { get; set; }
	}
}
