using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib.TwoWayCollections
{
	public interface ITwoWayCollection<T, PARENT> : ICollection<T>
	{
		PARENT Parent { get; set; }
	}
}
