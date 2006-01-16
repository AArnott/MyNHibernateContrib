using System;
using System.Collections.Generic;

using ExampleLibrary;

/// <summary>
/// Summary description for Dao
/// </summary>
public class Dao
{
	public IEnumerable<Tree> GetTrees()
	{
		return Database.GetList<Tree>();
	}

	public IEnumerable<Apple> GetApples(int treeId)
	{
		return Database.Get<Tree>(treeId).Apples;
	}
}
