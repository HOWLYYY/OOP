using System;

namespace NewBackups
{
	class Program
	{
		private static Backup backup1;

		static void Main(string[] args)
		{
			var res = 1;
			Console.Write("Enter backup's name: ");
			backup1 = new Backup(Console.ReadLine());
			Menu menu = new Menu();

			menu.AddMenuEntry("Exit", new Action(() => {; }));
			menu.AddMenuEntry("Print resourses", new Action(() => { backup1.PrintResourses(); }));
			menu.AddMenuEntry("Add file to resourse", new Action(AddFileToResourse));
			menu.AddMenuEntry("Delete file from resourse", new Action(DelFileFromResourse));
			menu.AddMenuEntry("Add rest point", new Action(AddRestPoint));
			menu.AddMenuEntry("Recovery backup to rest point", new Action(() => { RecoveryBackupToRestPoint(); }));

			while (res != 0)
			{
				res = menu.PrintMenu();
				if (res > 0)
					menu.ExecuteEntry(res);
			}
		}

		private static void AddFileToResourse()
		{
			Console.Write("Enter path to resourse: ");
			Console.WriteLine(backup1.AddResource(Console.ReadLine()) ? " - OK" : " - FAIL");
		}

		private static void DelFileFromResourse()
		{
			Console.Write("Enter path to resourse: ");
			Console.WriteLine(backup1.DelResource(Console.ReadLine()) ? " - OK" : " - FAIL");
		}

		private static TypeStorage GetTypeStorage()
		{
			Console.Write("Enter type of storage (arch / separate): ");
			return (Console.ReadLine() == "arch") ? TypeStorage.Arch : TypeStorage.Sep;
		}

		private static TypeRestPoint GetTypeRestPoint()
		{
			Console.Write("Enter type of rest point (full / part): ");
			return (Console.ReadLine() == "part") ? TypeRestPoint.Part : TypeRestPoint.Full;
		}

		private static void AddRestPoint()
		{
			backup1.CreateRestPoint(GetTypeRestPoint(), GetTypeStorage());
		}

		private static void RecoveryBackupToRestPoint()
		{
			backup1.PrintRestPoints();
			Console.Write("Enter name of rest point: ");
			Console.WriteLine(backup1.RecoveryBackupToRestPoint(Console.ReadLine()) ? " - OK" : " - FAIL");
		}
	}
}
