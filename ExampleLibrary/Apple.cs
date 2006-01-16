using System;

using MyNHibernateContrib.TwoWayCollections;

namespace ExampleLibrary
{
	/// <summary>
	/// Apples that could be found on a tree.
	/// </summary>
	public class Apple
		: IParentReferencingNode<Apple, Tree>
	{
		public Apple() { }
		public Apple(int seedCount)
		{
			SeedCount = seedCount;
		}

		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private int seedCount;
		/// <summary>
		/// The number of seeds in the apple.
		/// </summary>
		public virtual int SeedCount
		{
			get { return seedCount; }
			set { seedCount = value; }
		}

		private Tree tree;
		/// <summary>
		/// The tree that this apple is found on.
		/// </summary>
		/// <remarks>
		/// The setter is private because it is automatically
		/// set by adding the apple to the Tree's Apples collection.
		/// </remarks>
		public virtual Tree Tree
		{
			get { return tree; }
			private set { tree = value; }
		}

		#region IParentReferencingNode<Apple,Tree> Members

		Tree IParentReferencingNode<Apple, Tree>.Parent
		{
			get { return Tree; }
			set { Tree = value; }
		}

		#endregion
	}
}