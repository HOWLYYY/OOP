using System;
using System.Collections.Generic;

namespace Shops
{
    class ShopBase
    {
		private Dictionary<uint, Shop> shop_base;

		public Shop this[uint id]
		{
			get
			{
				if (shop_base.ContainsKey(id))
					return shop_base[id];
				else throw new Exception("No such shop");
			}
		}

        public ShopBase()
        {
			shop_base = new Dictionary<uint, Shop>();
		}

		public void AddShop(string name, string address)
		{
			Shop shop = new Shop(name, address);
			shop_base[shop.ID] = shop;
		}

		public uint CheapestListOfProducts(Dictionary<uint, double> Busket)
        {
			double sum = 0;
			double minSum = 0;
			uint cid = 0;
			foreach (var p in shop_base)
            {
				sum = p.Value.BuyListOfProducts(Busket);
				if (sum < minSum || minSum == 0)
                {
					minSum = sum;
					cid = p.Key;
                }
				if (minSum == 0)
					throw new Exception("No such shop");
            }
			return cid;
        }

		public void ShowBase()
		{
			foreach (var shop in shop_base)
			{
				Console.WriteLine("{0}", shop.Value.ToString());
				Console.WriteLine("-----------------------------------");
			}
		}
    }
}