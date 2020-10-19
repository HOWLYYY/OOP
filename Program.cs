using System;

class MainClass
{
	public static void Main()
	{
		var cmd = 1;
		IniFile fd;
		try
		{
			fd = new IniFile("test.ini");
		}
		catch (Exception e)
		{
			Console.WriteLine("{0}", e.Message);
			return;
		}

		while (cmd != 0)
		{
			Console.WriteLine("0) Exit{0}1) Show section{0}2) Show var{0}", Environment.NewLine);
			try
			{
				cmd = Int32.Parse(Console.ReadLine());
				string sec = "";

				if (cmd == 1)
				{
					Console.WriteLine("Input section name: ");
					sec = Console.ReadLine();

					fd.PrintSection(sec);
				}
				else if (cmd == 2)
				{
					Console.WriteLine("Input variable name: ");
					sec = Console.ReadLine();

					fd.PrintVar(sec);
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Incorrect command");
				cmd = 1;
			}
		}
	}
}