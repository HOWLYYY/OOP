using System;

namespace Shops
{
	class Program
	{
		static string GenerateStr(uint len)
		{
			Random rand = new Random();
			string res = "";
			for (uint i = 0; i < len; ++i)
			{
				res = string.Format("{0}{1}", res, (char)(rand.Next((int)'a', (int)'z' + 1)));
			}
			return res;
		}

		static void Main()
		{
			ShopBase sb = new ShopBase();
			ItemBase ib = new ItemBase();
			int shop_amount = 3;
			int item_amount = 10;
			var cmd = 1;

			for (int i = 0; i < shop_amount; ++i)
			{
				sb.AddShop(GenerateStr(10), GenerateStr(25));
			}
			sb.ShowBase();
			Console.WriteLine("#############################################");

			for (int i = 0; i < item_amount; ++i)
			{
				ib.AddItem(GenerateStr(5), GenerateStr(15));
			}
			ib.ShowBase();
			Console.WriteLine("#############################################");


			while (cmd != 0)
			{
				Console.WriteLine("0) Exit{0}1) Show ID {0}2) Show item name{0}3) Show address {0}4) Add Item to the shop {0}5) Get list of products on summ {0}6) buy list of products {0}7) Cheapest list {0}", Environment.NewLine);
				Int32.TryParse(Console.ReadLine(), out cmd);
				try
				{
					if (cmd == 1)
					{
						Console.WriteLine("Enter the name of item :");
						string name;
						name = Console.ReadLine();
						Console.WriteLine("ID of item : {0}", ib.GetItemID(name));
					}
					else if (cmd == 2)
					{
						Console.WriteLine("Enter ID of the item :");
						uint id;
						if (uint.TryParse(Console.ReadLine(), out id))
                        {
							Item item = ib[id];
							Console.WriteLine("Name of item : {0}", item.Name);
                        }
                        else
							Console.WriteLine("No such item");
					}
					else if (cmd == 3)
					{
						Console.WriteLine("Enter shop id :");
						uint id;
						if (uint.TryParse(Console.ReadLine(), out id))
						{
							Shop shop = sb[id];
							Console.WriteLine("Address of shop : {0}", shop.Address);
						}
						else
							Console.WriteLine("No such shop");
					}
					else if (cmd == 4)
                    {
						Console.WriteLine("Enter shop id :");
						uint shid, itid;
						double amount, price;
						if (uint.TryParse(Console.ReadLine(), out shid))
                        {
							Shop shop = sb[shid];
							Console.WriteLine("Enter item id :");
							if (uint.TryParse(Console.ReadLine(), out itid))
                            {
								Console.WriteLine("Enter amount of item : ");
								if (double.TryParse(Console.ReadLine(), out amount))
                                {
									Console.WriteLine("Enter price of item : ");
									if (double.TryParse(Console.ReadLine(), out price))
                                    {
										shop.AddItem(itid, amount, price);
                                    }
                                }
							}
                        }
					}
					else if (cmd == 5)
                    {
						Console.WriteLine("Enter shop id :");
						uint id;
						double summ;
						if (uint.TryParse(Console.ReadLine(), out id))
                        {
							Shop shop = sb[id];
							Console.WriteLine("Enter summ :");
							if (Double.TryParse(Console.ReadLine(), out summ))
                            {
								shop.ListOfProductOnPrice(id, summ);
                            }
                        }
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
}