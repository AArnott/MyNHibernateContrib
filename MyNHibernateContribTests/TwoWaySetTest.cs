using MyNHibernateContrib.TwoWayCollections;
using Iesi.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MyNHibernateContribTests
{
	/// <summary>
	/// This is a test class for MyNHibernateContrib.TwoWayCollections.TwoWaySet[T, PARENT] and is intended
	/// to contain all MyNHibernateContrib.TwoWayCollections.TwoWaySet[T, PARENT] Unit Tests
	/// </summary>
	[TestClass()]
	public class TwoWaySetTest
	{


		private TestContext testContextInstance;

		/// <summary>
		/// Gets or sets the test context which provides
		/// information about and functionality for the current test run.
		/// </summary>
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
		/// Initialize() is called once during test execution before
		/// test methods in this test class are executed.
		/// </summary>
		[TestInitialize()]
		public void Initialize()
		{
			//  TODO: Add test initialization code
		}

		/// <summary>
		/// Cleanup() is called once during test execution after
		/// test methods in this class have executed unless
		/// this test class' Initialize() method throws an exception.
		/// </summary>
		[TestCleanup()]
		public void Cleanup()
		{
			//  TODO: Add test cleanup code
		}


		/// <summary>
		/// A test case for TwoWaySet (PARENT, ISet)
		/// </summary>
		[TestMethod()]
		public void ChildReferencingParentTest()
		{
			ParentNode parent = new ParentNode();
			ChildNode child = new ChildNode();

			parent.Children.Add(child);
			Assert.AreSame(parent, child.Parent);

			parent.Children.Remove(child);
			Assert.IsNull(child.Parent);
		}
	}
}
