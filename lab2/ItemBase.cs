using System;
using System.Collections.Generic;

namespace Shops
{
    class ItemBase
    {
		private Dictionary<uint, Item> item_base;

		public Item this[uint id]
		{
			get
			{
				if (item_base.ContainsKey(id))
					return item_base[id];
				return null;
			}
		}

        public ItemBase()
        {
			item_base = new Dictionary<uint, Item>();
		}

		public void AddItem(string name, string description)
		{
			Item item = new Item(name, description);
			item_base[item.ID] = item;
		}

		public long GetItemID(string name)
		{
			foreach (var item in item_base)
			{
				if (item.Value.Name == name)
					return item.Key;
			}
			throw new Exception("Failed to get ID");
		}

		public void ShowBase()
		{
			foreach (var item in item_base)
			{
				Console.WriteLine("{0}", item.Value.ToString());
				Console.WriteLine("-----------------------------------");
			}
		}

    }
}