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

			menu.AddMenuEntry("Exit", new Action(() => { }));
			menu.AddMenuEntry("Print resourses", new Action(() => { backup1.PrintResourses(); }));
			menu.AddMenuEntry("Add file to resourse", new Action(AddFileToResourse));
			menu.AddMenuEntry("Delete file from resourse", new Action(DelFileFromResourse));
			menu.AddMenuEntry("Add rest point", new Action(AddRestPoint));
			menu.AddMenuEntry("Recovery backup to rest point", new Action(RecoveryBackupToRestPoint));
			menu.AddMenuEntry("Add cleaner to rest point", new Action(AddCleanerToRestPoint));


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

		private static void AddCleanerToRestPoint()
		{
			Console.Write("Enter type of cleaner of rest point (size / date / amount / hybrid): ");
			switch (Console.ReadLine())
			{
				case "size":
					Console.Write("Enter max size of backup: ");
					if (!Int32.TryParse(Console.ReadLine(), out backup1.max_size))
						backup1.max_size = -1;
					break;
				case "date":
					Console.Write("Enter min DateTime of backup: ");
					backup1.max_date = Convert.ToDateTime(Console.ReadLine());
					break;
				case "amount":
					Console.Write("Enter max amount of rest points: ");
					if (!Int32.TryParse(Console.ReadLine(), out backup1.max_amount))
						backup1.max_amount = -1;
					break;
				case "hybrid":
					// Console.Write("Enter max size of backup: ");
					// Console.ReadLine();
					break;
				default:
					Console.WriteLine("Error type");
					break;
			}
		}
	}
}
