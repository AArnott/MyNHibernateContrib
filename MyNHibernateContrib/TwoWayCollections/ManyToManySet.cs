using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib.TwoWayCollections
{
	public class ManyToManySet<T, O> :
		GenericCollections.ISetWrapper<T>,
		ITwoWayCollection<T, O>
		where T : IManyToManyNode<T, O>
		where O : IManyToManyNode<O, T>
	{
		public ManyToManySet(O parent, Iesi.Collections.ISet set)
			: base(set)
		{
			Parent = parent;
		}

		private O parent;
		public O Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		protected override void OnAdded(T item)
		{
			base.OnAdded(item);
			if (item == null) return;
			if (!item.Collection.Contains(parent))
				item.Collection.Add(parent);
		}

		protected override void OnRemoved(T item)
		{
			base.OnRemoved(item);
			if (item.Collection.Contains(Parent))
				item.Collection.Remove(Parent);
		}
	}
}
