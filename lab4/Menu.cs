using System;
using System.Collections;
using System.Collections.Generic;

namespace NewBackups
{
	public class Menu
	{
		private Dictionary<int, MenuEntry> menuEntries;
		private int last;

		public Menu()
		{
			menuEntries = new Dictionary<int, MenuEntry>();
			last = 0;
		}

		public void AddMenuEntry(string descr, Action act)
		{
			menuEntries.Add(last, new MenuEntry(descr, act));
			last++;
		}

		public int PrintMenu()
		{
			int cmd = -1;
			foreach (var menuEntry in menuEntries)
			{
				Console.WriteLine("{0}) {1}", menuEntry.Key, menuEntry.Value.Description);
			}
			if (!Int32.TryParse(Console.ReadLine(), out cmd))
				cmd = -1;
			return cmd;
		}

		public void ExecuteEntry(int cmd)
		{
			menuEntries[cmd].ExecuteEntry();
		}
	}
}
