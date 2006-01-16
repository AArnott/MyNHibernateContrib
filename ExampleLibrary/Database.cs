using System;
using System.Collections.Generic;
using System.Text;

using MyNHibernateContrib;

namespace ExampleLibrary
{
	public class Database : DbMgr
	{
		static Database()
		{
			InitializeConfiguration += new InitializeConfigurationEventHandler(Database_InitializeConfiguration);
			InitializeData += new InitializeDataEventHandler(Database_InitializeData);
		}

		static void Database_InitializeConfiguration(NHibernate.Cfg.Configuration config)
		{
			config.AddAssembly(typeof(Apple).Assembly);
		}

		static void Database_InitializeData()
		{
			Tree tree1 = new Tree();
			tree1.Name = "My first tree";
			tree1.Apples.Add(new Apple(5));
			tree1.Apples.Add(new Apple(2));
			Session.Save(tree1);

			Tree tree2 = new Tree();
			tree2.Name = "My second tree";
			tree2.Apples.Add(new Apple(15));
			tree2.Apples.Add(new Apple(12));
			Session.Save(tree2);
		}
	}
}
