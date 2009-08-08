using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;

using Iesi.Collections;
using MyNHibernateContrib.TwoWayCollections;

namespace MyNHibernateContribTests
{
	class User : IManyToManyNode<User, Role>
	{
		public User()
		{
			Roles = new ManyToManySet<Role, User>(this, new ListSet());
		}

		public readonly ManyToManySet<Role, User> Roles;

		#region IManyToManyNode<User,Role> Members

		ManyToManySet<Role, User> IManyToManyNode<User, Role>.Collection
		{
			get { return Roles; }
		}

		#endregion
	}

	class Role : IManyToManyNode<Role, User>
	{
		public Role()
		{
			Users = new ManyToManySet<User, Role>(this, new ListSet());
		}

		public readonly ManyToManySet<User, Role> Users;

		#region IManyToManyNode<Role,User> Members

		ManyToManySet<User, Role> IManyToManyNode<Role, User>.Collection
		{
			get { return Users; }
		}

		#endregion
	}

	/// <summary>
	/// This is a test class for MyNHibernateContrib.TwoWayCollections.ManyToManySet&lt;T, O&gt; and is intended
	/// to contain all MyNHibernateContrib.TwoWayCollections.ManyToManySet&lt;T, O&gt; Unit Tests
	/// </summary>
	[TestClass()]
	public class ManyToManySetTest
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
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		/// A test for ManyToManySet (Iesi.Collections.ISet)
		/// </summary>
		[TestMethod()]
		public void SimpleAddTest1()
		{
			Role role = new Role();
			User user = new User();

			role.Users.Add(user);
			Assert.IsTrue(role.Users.Contains(user));
		}

		/// <summary>
		/// A test for ManyToManySet (Iesi.Collections.ISet)
		/// </summary>
		[TestMethod()]
		public void SimpleAddTest2()
		{
			Role role = new Role();
			User user = new User();

			user.Roles.Add(role);
			Assert.IsTrue(user.Roles.Contains(role));
		}

		[TestMethod]
		public void AddAndBackTest1()
		{
			Role role = new Role();
			User user = new User();

			user.Roles.Add(role);
			Assert.IsTrue(user.Roles.Contains(role));
			Assert.IsTrue(role.Users.Contains(user));
		}

		[TestMethod]
		public void AddAndBackTest2()
		{
			Role role = new Role();
			User user = new User();

			role.Users.Add(user);
			Assert.IsTrue(role.Users.Contains(user));
			Assert.IsTrue(user.Roles.Contains(role));
		}

		[TestMethod]
		public void AddThenRemoveTest()
		{
			Role role = new Role();
			User user = new User();

			role.Users.Add(user);
			role.Users.Remove(user);

			Assert.AreEqual(0, role.Users.Count);
			Assert.AreEqual(0, user.Roles.Count);
		}

		[TestMethod]
		public void AddThenRemoveFromOtherTest()
		{
			Role role = new Role();
			User user = new User();

			role.Users.Add(user);
			user.Roles.Remove(role);

			Assert.AreEqual(0, role.Users.Count);
			Assert.AreEqual(0, user.Roles.Count);
		}
	}
}
