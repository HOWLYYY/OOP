using System;
using System.Collections.Generic;

namespace Shops
{
	class Pair
	{
		public double Price
		{
			get { return _price; }
			set
			{
				if (value >= 0)
					_price = value;
				else
					throw new Exception("Negative price");
			}
		}

		public double Amount { get; set; }

		private double _price;

		public Pair(double price = 0, double amount = 0)
		{
			_price = (price < 0) ? 0 : price;
			Amount = (amount < 0) ? 0 : amount;
		}
	}

    class Shop
    {	
        public uint ID { get; }
        public string Name { get; set; }
        public string Address { get; set; }

		private Dictionary<uint, Pair> items;
		private static uint _id = 0;

		public double this[uint id]
		{
			get
			{
				if (items.ContainsKey(id))
					return items[id].Price;
				return 0;
			}
			set
			{
				items[id].Price = value;
			}
		}

        public Shop(string name, string address)
        {
			items = new Dictionary<uint, Pair>();

            ID = _id++;
            Name = name;
            Address = address;
        }

		public void AddItem(uint id, double amount, double price)
        {
			if (!items.ContainsKey(id))
			{
				items.Add(id, new Pair(price));
			}
			else
				throw new Exception("Already in the shop");
			items[id].Amount += amount;
        }

		public void ListOfProductOnPrice(uint id, double sum)
		{
			foreach (var item in items)
			{
				if (item.Value.Price > 0)
				{
					double amount = sum / item.Value.Price;
					Console.WriteLine("Items on Price : {0} {1}", item.Key, amount);
				}
				else 
					throw new Exception("The price of item is negative");
			}
		}

		public double BuyListOfProducts(Dictionary<uint, double>Busket)
        {
			double sum = 0;
			foreach(var item in Busket)
            {
				if (!items.ContainsKey(item.Key) || item.Value > items[item.Key].Amount)
                {
					throw new Exception("Not enough items in shop");
                }
                else
                {
					sum += item.Value * items[item.Key].Price;
                }
            }
			return sum;
        }

		public override string ToString()
		{
			return string.Format("\t{1}{0}\t{2}{0}\t{3}{0}", Environment.NewLine, ID, Name, Address);
		}
    }
}