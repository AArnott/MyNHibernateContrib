using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyNHibernateContrib.GenericCollections;
using Iesi.Collections;

namespace MyNHibernateContribTests
{
	/// <summary>
	/// This is a test class for MyNHibernateContrib.ISetWrapper[T] and is intended
	/// to contain all MyNHibernateContrib.ISetWrapper[T] Unit Tests
	/// </summary>
	[TestClass()]
	public class ISetWrapperTest
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
		/// A test case for Add (T)
		/// </summary>
		[TestMethod()]
		public void AddTest()
		{
			ISet set = new Iesi.Collections.ListSet();
			ICollection<int> target = new ISetWrapper<int>(set);

			int item = 5;
			target.Add(item);

			Assert.AreEqual(1, set.Count);
			Assert.IsTrue(set.Contains(item));
		}


		/// <summary>
		/// A test case for Count
		/// </summary>
		[TestMethod()]
		public void CountTest()
		{
			ISet set = new Iesi.Collections.ListSet();
			ICollection<int> target = new ISetWrapper<int>(set);

			Assert.AreEqual(set.Count, target.Count);
			target.Add(5);
			Assert.AreEqual(set.Count, target.Count);
		}

		/// <summary>
		/// A test case for Remove (T)
		/// </summary>
		[TestMethod()]
		public void RemoveTest()
		{
			ISet set = new Iesi.Collections.ListSet();
			ICollection<int> target = new ISetWrapper<int>(set);

			int item = 5;
			target.Add(item);
			target.Remove(item);

			Assert.AreEqual(0, set.Count);
			Assert.IsFalse(set.Contains(item));
		}

		/// <summary>
		/// A test case for Clear ()
		/// </summary>
		[TestMethod()]
		public void ClearTest()
		{
			ISet set = new Iesi.Collections.ListSet();
			ICollection<int> target = new ISetWrapper<int>(set);

			for (int i = 0; i < 5; i++)
				target.Add(i);

			target.Clear();
			Assert.AreEqual(0, set.Count);
			Assert.AreEqual(0, target.Count);
		}

		/// <summary>
		///A test case for CopyTo (T[], int)
		///</summary>
		[TestMethod()]
		public void CopyToTest()
		{
			ISet set = new Iesi.Collections.ListSet();
			ICollection<int> target = new ISetWrapper<int>(set);

			for (int i = 0; i < 5; i++)
				target.Add(i);

			int[] array = new int[target.Count];
			target.CopyTo(array, 0);

			for (int i = 0; i < target.Count; i++)
				Assert.IsTrue(target.Contains(array[i]));
		}
	}
}
