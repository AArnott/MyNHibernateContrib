using System;

using Iesi.Collections;
using MyNHibernateContrib.GenericCollections;
using MyNHibernateContrib.TwoWayCollections;

namespace ExampleLibrary
{
	/// <summary>
	/// Summary description for Tree
	/// </summary>
	public class Tree
		: IReferenceableParent<Apple, Tree>
	{
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private string name;
		/// <summary>
		/// Some name/description of the tree.
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private ISet applesSet;
		protected ISet ApplesSet
		{
			get
			{
				if (applesSet == null) applesSet = new HashedSet();
				return applesSet;
			}
			set
			{
				applesSet = value;
				apples = null;
			}
		}
		private TwoWaySet<Apple, Tree> apples;
		/// <summary>
		/// A collection of apples on the tree.
		/// </summary>
		public virtual TwoWaySet<Apple, Tree> Apples
		{
			get
			{
				if (apples == null)
					apples = new TwoWaySet<Apple, Tree>(this, ApplesSet);
				return apples;
			}
		}

		#region IReferenceableParent<Apple,Tree> Members

		ITwoWayCollection<Apple, Tree> IReferenceableParent<Apple, Tree>.ContainingCollection
		{
			get { return Apples; }
		}

		#endregion
	}
}