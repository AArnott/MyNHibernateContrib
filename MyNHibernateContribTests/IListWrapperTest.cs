using System.Collections.Generic;
using MyNHibernateContrib.GenericCollections;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MyNHibernateContribTests
{
	/// <summary>
	///This is a test class for MyNHibernateContrib.GenericCollections.IListWrapper[T] and is intended
	///to contain all MyNHibernateContrib.GenericCollections.IListWrapper[T] Unit Tests
	///</summary>
	[TestClass()]
	public class IListWrapperTest
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
		/// A test case for IndexOf (T)
		/// </summary>
		[TestMethod()]
		public void IndexOfTest()
		{
			IList list = new ArrayList();
			for (int i = 1; i <= 3; i++)
				list.Add(i);

			IListWrapper<int> target = new IListWrapper<int>(list);

			for (int i = 0; i < list.Count; i++)
				Assert.AreEqual(i, target.IndexOf(i + 1));
		}

		/// <summary>
		/// A test case for Insert (int, T)
		/// </summary>
		[TestMethod()]
		public void InsertTest()
		{
			IList list = new ArrayList();
			for (int i = 1; i <= 3; i++)
				list.Add(i);

			IListWrapper<int> target = new IListWrapper<int>(list);

			int item = 5;
			target.Insert(1, item);

			Assert.AreEqual(item, list[1]);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(2, list[2]);
		}

		/// <summary>
		/// A test case for Remove (T)
		/// </summary>
		[TestMethod()]
		public void RemoveTest()
		{
			IList list = new ArrayList();
			for (int i = 1; i <= 3; i++)
				list.Add(i);

			IListWrapper<int> target = new IListWrapper<int>(list);

			target.Remove(2);

			Assert.AreEqual(2, list.Count);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(3, list[1]);
		}

		/// <summary>
		/// A test case for RemoveAt (int)
		/// </summary>
		[TestMethod()]
		public void RemoveAtTest()
		{
			IList list = new ArrayList();
			for (int i = 1; i <= 3; i++)
				list.Add(i);

			IListWrapper<int> target = new IListWrapper<int>(list);

			target.RemoveAt(1);

			Assert.AreEqual(2, list.Count);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(3, list[1]);
		}

		/// <summary>
		/// A test case for this[int index]
		/// </summary>
		[TestMethod()]
		public void ItemTest()
		{
			IList list = new ArrayList();
			for (int i = 1; i <= 3; i++)
				list.Add(i);

			IListWrapper<int> target = new IListWrapper<int>(list);

			for (int i = 0; i < list.Count; i++)
				Assert.AreEqual(list[i], target[i]);

			int item = 5;
			target[1] = item;
			Assert.AreEqual(item, list[1]);
		}

		/// <summary>
		/// A test case for Contains (T)
		/// </summary>
		[TestMethod()]
		public void ContainsTest()
		{
			IList list = new ArrayList();
			for (int i = 1; i <= 3; i++)
				list.Add(i);

			IListWrapper<int> target = new IListWrapper<int>(list);

			Assert.IsTrue(target.Contains(2));
			Assert.IsFalse(target.Contains(0));
		}

		/// <summary>
		///A test case for Clear ()
		///</summary>
		[TestMethod()]
		public void ClearTest()
		{
			IList list = new ArrayList();
			for (int i = 1; i <= 3; i++)
				list.Add(i);

			IListWrapper<int> target = new IListWrapper<int>(list);

			target.Clear();
			Assert.AreEqual(0, list.Count);
		}

		/// <summary>
		/// A test case for Add (T)
		/// </summary>
		[TestMethod()]
		public void AddTest()
		{
			IList list = new ArrayList();
			IListWrapper<int> target = new IListWrapper<int>(list);
			
			for (int i = 1; i <= 3; i++)
				target.Add(i);

			Assert.AreEqual(3, list.Count);

			for (int i = 1; i <= 3; i++)
				Assert.AreEqual(i, list[i - 1]);
		}

		/// <summary>
		/// A test case for Count
		/// </summary>
		[TestMethod()]
		public void CountTest()
		{
			IList list = new ArrayList();
			IListWrapper<int> target = new IListWrapper<int>(list);

			Assert.AreEqual(list.Count, target.Count);
			for (int i = 1; i <= 3; i++)
				list.Add(i);
			Assert.AreEqual(list.Count, target.Count);
		}

		/// <summary>
		/// A test case for CopyTo (T[], int)
		/// </summary>
		[TestMethod()]
		public void CopyToTest()
		{
			IList list = new ArrayList();
			for (int i = 1; i <= 3; i++)
				list.Add(i);

			IListWrapper<int> target = new IListWrapper<int>(list);
			int[] array = new int[list.Count];
			target.CopyTo(array, 0);

			for (int i = 1; i <= 3; i++)
				Assert.AreEqual(i, list[i - 1]);
		}

		/// <summary>
		/// A test case for GetEnumerator ()
		/// </summary>
		[TestMethod()]
		public void GetEnumeratorTest()
		{
			IList list = new ArrayList();
			for (int i = 1; i <= 3; i++)
				list.Add(i);

			IListWrapper<int> target = new IListWrapper<int>(list);
			int counter = 0;
			foreach (int i in target)
				Assert.AreEqual(++counter, i);
		}

	}


}
