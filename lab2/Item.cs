using System;

namespace Shops
{
    class Item
    {
        public uint ID { get; }
        public string Name { get; set; }
        public string Description { get; set; }

		private static uint _id = 0;

        public Item(string name, string description)
        {
            ID = _id++;
            Name = name;
            Description = description;
        }

		public override string ToString()
		{
			return string.Format("\t{1}{0}\t{2}{0}\t{3}{0}", Environment.NewLine, ID, Name, Description);
		}
    }
}