using MyNHibernateContrib.TwoWayCollections;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MyNHibernateContribTests
{
	/// <summary>
	///This is a test class for MyNHibernateContrib.TwoWayCollections.TwoWayList[T, PARENT] and is intended
	///to contain all MyNHibernateContrib.TwoWayCollections.TwoWayList[T, PARENT] Unit Tests
	///</summary>
	[TestClass()]
	public class TwoWayListTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		/// <summary>
		///Initialize() is called once during test execution before
		///test methods in this test class are executed.
		///</summary>
		[TestInitialize()]
		public void Initialize()
		{
			//  TODO: Add test initialization code
		}

		/// <summary>
		///Cleanup() is called once during test execution after
		///test methods in this class have executed unless
		///this test class' Initialize() method throws an exception.
		///</summary>
		[TestCleanup()]
		public void Cleanup()
		{
			//  TODO: Add test cleanup code
		}


		/// <summary>
		/// A test case for TwoWayList (PARENT, ISet)
		/// </summary>
		[TestMethod()]
		public void ChildReferencingParentTest()
		{
			ParentNode parent = new ParentNode();
			ChildNode child1 = new ChildNode();
			ChildNode child2 = new ChildNode();

			parent.AdoptedChildren.Add(child1);
			parent.AdoptedChildren.Add(child2);
			Assert.AreSame(parent, child1.Parent);
			Assert.AreSame(parent, child2.Parent);

			Assert.AreSame(child1, parent.AdoptedChildren[0]);
			Assert.AreSame(child2, parent.AdoptedChildren[1]);

			parent.AdoptedChildren.Remove(child1);
			Assert.IsNull(child1.Parent);
			Assert.AreSame(parent, child2.Parent);
		}
	}
}
