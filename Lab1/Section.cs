using System;
using System.Collections.Generic;

public class Section
{
    private readonly Dictionary<string, string> vars;

	public Section()
	{
		vars = new Dictionary<string, string>();
	}

	public bool AddVar(string name, string val)
	{
		if (!vars.ContainsKey(name))
		{
			vars[name] = val;
			return true;
		}
		return false;
	}

	public bool PrintVar(string name)
	{
		if (vars.ContainsKey(name))
		{
			Console.WriteLine("{0} = {1}", name, vars[name]);
			return true;
		}
		return false;
	}

	public void PrintSection()
	{
		foreach (var i in vars)
		{
			Console.WriteLine("{0} = {1}", i.Key, i.Value);
		}
	}
}
