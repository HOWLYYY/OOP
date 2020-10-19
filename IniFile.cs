using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class IniFile
{
    private readonly Dictionary<string, Section> sections;

	public IniFile()
	{
		sections = new Dictionary<string, Section>();
	}

	public IniFile(string name)
	{
		var r_section = new Regex(@"^[[a-zA-Z0-9_]+]$");
		var r_var = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*[\s]*=[\s]*[^\s]+");
		sections = new Dictionary<string, Section>();

        using var sr = new StreamReader(name);
        string line = "";
        string last_sec = "";

        while ((line = sr.ReadLine()) != null)
        {
			line = line.Trim();
            if (r_section.IsMatch(line))
            {
                string temp = r_section.Match(line.Trim()).Value;

				if (AddSection(temp))
					last_sec = temp;
				else
					throw new Exception("Incorrect ini file.");
            }
            else if (r_var.IsMatch(line))
            {
                var res = r_var.Match(line.Trim()).Value.Split('=');

                if (String.IsNullOrEmpty(last_sec))
					throw new Exception("Incorrect ini file.");
				else if (!AddVar(last_sec, res[0].Trim(), res[1].Trim()))
					throw new Exception("Incorrect ini file.");
			}
			else if (!String.IsNullOrEmpty(line) && line[0] != ';')
				throw new Exception("Incorrect ini file.");
		}
    }

	public void PrintSection(string name)
	{
		if (sections.ContainsKey(name))
			sections[name].PrintSection();
		else Console.WriteLine("No such section");
	}

	public void PrintVar(string name)
	{
		bool flag = true;
		foreach (var s in sections)
		{
			if (s.Value.PrintVar(name))
				flag = false;
		}
		if (flag)
			Console.WriteLine("Variable not found");
	}

	public bool AddSection(string name)
	{
		if (!sections.ContainsKey(name))
		{
			sections[name] = new Section();
			return true;
		}
		return false;
	}

	public bool AddVar(string section, string name, string value)
	{
		if (!sections.ContainsKey(name))
			return sections[section].AddVar(name, value);
		return false;
	}
}
