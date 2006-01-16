using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib.TwoWayCollections
{
	public interface IManyToManyNode<T, O>
		where T : IManyToManyNode<T, O>
		where O : IManyToManyNode<O, T>
	{
		ManyToManySet<O, T> Collection { get; }
	}
}
