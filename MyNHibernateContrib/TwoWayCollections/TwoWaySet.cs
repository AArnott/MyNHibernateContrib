using System;
using System.Collections.Generic;
using System.Text;

using Iesi.Collections;

namespace MyNHibernateContrib.TwoWayCollections
{
	public class TwoWaySet<CHILD, PARENT> : 
		GenericCollections.ISetWrapper<CHILD>,
		ITwoWayCollection<CHILD, PARENT>
		where CHILD : class, IParentReferencingNode<CHILD, PARENT>
		where PARENT : class, IReferenceableParent<CHILD, PARENT>
	{

		public TwoWaySet(PARENT parent, ISet set) : base(set)
		{
			Parent = parent;
		}

		private PARENT parent;
		public PARENT Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		protected override void OnAdding(CHILD item)
		{
			base.OnAdding(item);
			if (item == null) return;
			if (item.Parent != null && item.Parent != Parent)
				item.Parent.ContainingCollection.Remove(item);
			item.Parent = Parent;
		}

		protected override void OnRemoving(CHILD item)
		{
			base.OnRemoving(item);
			if (item != null)
				item.Parent = null;
		}

	}
}
