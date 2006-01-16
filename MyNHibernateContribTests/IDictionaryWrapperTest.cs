using System;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyNHibernateContrib.GenericCollections;

namespace MyNHibernateContribTests
{
	/// <summary>
	///This is a test class for MyNHibernateContrib.GenericCollections.IDictionaryWrapper[TKey, TValue] and is intended
	///to contain all MyNHibernateContrib.GenericCollections.IDictionaryWrapper[TKey, TValue] Unit Tests
	///</summary>
	[TestClass()]
	public class IDictionaryWrapperTest
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
		/// A test case for Add (TKey, TValue)
		/// </summary>
		[TestMethod()]
		public void AddTest()
		{
			// IDictionary dictionary = null; // TODO: Initialize to an appropriate value
			// 
			// IDictionaryWrapper target = new IDictionaryWrapper(dictionary);
			// 
			// TKey key = 0; // TODO: Initialize to an appropriate value
			// 
			// TValue value = 0; // TODO: Initialize to an appropriate value
			// 
			// target.Add(key, value);
			// 
			// Assert.Inconclusive("A method that does not return a value cannot be verified.");
			Assert.Inconclusive("Generics testing must be manually provided.");
		}

		/// <summary>
		/// A test case for ContainsKey (TKey)
		/// </summary>
		[TestMethod()]
		public void ContainsKeyTest()
		{
			// IDictionary dictionary = null; // TODO: Initialize to an appropriate value
			// 
			// IDictionaryWrapper target = new IDictionaryWrapper(dictionary);
			// 
			// TKey key = 0; // TODO: Initialize to an appropriate value
			// 
			// bool expected = false;
			// bool actual;
			// 
			// actual = target.ContainsKey(key);
			// 
			// Assert.AreEqual(expected, actual, "MyNHibernateContrib.GenericCollections.IDictionaryWrapper[TKey, TValue].ContainsK" +
			//        "ey did not return the expected value.");
			// Assert.Inconclusive("Verify the correctness of this test method.");
			Assert.Inconclusive("Generics testing must be manually provided.");
		}

		/// <summary>
		/// A test case for Clear ()
		/// </summary>
		[TestMethod()]
		public void ClearTest()
		{
			// IDictionary dictionary = null; // TODO: Initialize to an appropriate value
			// 
			// IDictionaryWrapper target = new IDictionaryWrapper(dictionary);
			// 
			// target.Clear();
			// 
			// Assert.Inconclusive("A method that does not return a value cannot be verified.");
			Assert.Inconclusive("Generics testing must be manually provided.");
		}

		/// <summary>
		/// A test case for Remove (TKey)
		/// </summary>
		[TestMethod()]
		public void RemoveTest()
		{
			// IDictionary dictionary = null; // TODO: Initialize to an appropriate value
			// 
			// IDictionaryWrapper target = new IDictionaryWrapper(dictionary);
			// 
			// TKey key = 0; // TODO: Initialize to an appropriate value
			// 
			// bool expected = false;
			// bool actual;
			// 
			// actual = target.Remove(key);
			// 
			// Assert.AreEqual(expected, actual, "MyNHibernateContrib.GenericCollections.IDictionaryWrapper[TKey, TValue].Remove di" +
			//        "d not return the expected value.");
			// Assert.Inconclusive("Verify the correctness of this test method.");
			Assert.Inconclusive("Generics testing must be manually provided.");
		}

		/// <summary>
		/// A test case for this[TKey key]
		/// </summary>
		[TestMethod()]
		public void ItemTest()
		{
			// IDictionary dictionary = null; // TODO: Initialize to an appropriate value
			// 
			// IDictionaryWrapper target = new IDictionaryWrapper(dictionary);
			// 
			// TValue val = 0; // TODO: Assign to an appropriate value for the property
			// 
			// TKey key = 0; // TODO: Initialize to an appropriate value
			// 
			// target[key] = val;
			// 
			// 
			// Assert.AreEqual(val, target[key], "MyNHibernateContrib.GenericCollections.IDictionaryWrapper[TKey, TValue].this was " +
			//        "not set correctly.");
			// Assert.Inconclusive("Verify the correctness of this test method.");
			Assert.Inconclusive("Generics testing must be manually provided.");
		}

		/// <summary>
		/// A test case for Count
		/// </summary>
		[TestMethod()]
		public void CountTest()
		{
			// IDictionary dictionary = null; // TODO: Initialize to an appropriate value
			// 
			// IDictionaryWrapper target = new IDictionaryWrapper(dictionary);
			// 
			// int val = 0; // TODO: Assign to an appropriate value for the property
			// 
			// 
			// Assert.AreEqual(val, target.Count, "MyNHibernateContrib.GenericCollections.IDictionaryWrapper[TKey, TValue].Count was" +
			//        " not set correctly.");
			// Assert.Inconclusive("Verify the correctness of this test method.");
			Assert.Inconclusive("Generics testing must be manually provided.");
		}

		[TestMethod()]
		public void EnumeratorTest()
		{
			ListDictionary dict = new ListDictionary();
			dict.Add("a", 5);

			IDictionaryWrapper<string, int> genDict = new IDictionaryWrapper<string, int>(dict);
			int count = 0;
			foreach (System.Collections.Generic.KeyValuePair<string, int> pair in genDict)
			{
				Assert.AreEqual("a", pair.Key);
				Assert.AreEqual(5, pair.Value);
				Assert.AreEqual(1, ++count);
			}
		}

	}


}
