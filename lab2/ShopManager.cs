namespace Shops
{
    class ShopManager
    {
        private ShopBase sb;
        private ItemBase ib;
        public ShopManager()
        {
            sb = new ShopBase();
            ib = new ItemBase();
        }

        public bool AddShop(string name, string adress)
        {
            sb.AddShop(name, adress);
            return true;
        }

        public bool AddItem(uint id, double amount, double price, string name, string description)
        {
            ib.AddItem(name, description);
            return true;
        }

        public bool AddItemToShop(uint id, double amount, double price)
        {
            sb[id].AddItem(id, amount, price);
            return true;
        }

    }
}
