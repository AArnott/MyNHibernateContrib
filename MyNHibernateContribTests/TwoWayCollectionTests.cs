using System;
using System.Collections.Generic;
using System.Text;

using Iesi.Collections;
using MyNHibernateContrib.TwoWayCollections;

namespace MyNHibernateContribTests
{
	class ParentNode : IReferenceableParent<ChildNode, ParentNode>
	{
		private ISet childrenSet;
		private ISet ChildrenSet
		{
			get
			{
				if (childrenSet == null) childrenSet = new ListSet();
				return childrenSet;
			}
			set
			{
				childrenSet = value;
				children = null;
			}
		}
		private ITwoWayCollection<ChildNode, ParentNode> children;
		public ITwoWayCollection<ChildNode, ParentNode> Children
		{
			get
			{
				if (children == null)
					children = new TwoWaySet<ChildNode, ParentNode>(this, ChildrenSet);
				return children;
			}
		}

		private System.Collections.IList adoptedChildrenList;
		private System.Collections.IList AdoptedChildrenList
		{
			get
			{
				if (adoptedChildrenList == null) adoptedChildrenList = new System.Collections.ArrayList();
				return adoptedChildrenList;
			}
			set
			{
				adoptedChildrenList = value;
				adoptedChildren = null;
			}
		}
		private IList<ChildNode> adoptedChildren;
		public IList<ChildNode> AdoptedChildren
		{
			get
			{
				if (adoptedChildren == null)
					adoptedChildren = new TwoWayList<ChildNode, ParentNode>(this, AdoptedChildrenList);
				return adoptedChildren;
			}
		}

		#region IReferenceableParent<ChildNode,ParentNode> Members

		ITwoWayCollection<ChildNode, ParentNode> IReferenceableParent<ChildNode, ParentNode>.ContainingCollection
		{
			get { return Children; }
		}

		#endregion
	}

	class ChildNode : IParentReferencingNode<ChildNode, ParentNode>
	{
		ParentNode parent;
		public ParentNode Parent
		{
			get { return parent; }
			protected set { parent = value; }
		}


		#region IParentReferencingNode<ChildNode,ParentNode> Members

		ParentNode IParentReferencingNode<ChildNode, ParentNode>.Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		#endregion
	}
}
