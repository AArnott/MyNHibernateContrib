using System;
using System.Collections.Generic;
using System.Text;

using Iesi.Collections;

namespace MyNHibernateContrib.TwoWayCollections
{
	public class TwoWayDictionary<TKey, TValue, PARENT> :
		GenericCollections.IDictionaryWrapper<TKey, TValue>,
		ITwoWayDictionary<TKey, TValue, PARENT>
		where TValue : class, IParentReferencingNode<TValue, PARENT>
		where PARENT : class, IReferenceableParentDictionary<TKey, TValue, PARENT>
	{
		public TwoWayDictionary(PARENT parent, System.Collections.IDictionary dictionary)
			: base(dictionary)
		{
			Parent = parent;
		}

		private PARENT parent;
		public PARENT Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		protected override void OnAdding(TKey key, TValue item)
		{
			base.OnAdding(key, item);
			if (item == null) return;
			if (item.Parent != null && item.Parent != Parent)
				item.Parent.ContainingDictionary.Remove(key);
			item.Parent = Parent;
		}

		protected override void OnRemoving(TKey key, TValue item)
		{
			base.OnRemoving(key, item);
			if (item != null)
				item.Parent = null;
		}

	}
}
