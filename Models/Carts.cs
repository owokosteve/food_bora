namespace foodbora
{
    public class Cart
    {
        public string ItemId { get; set; }
        private static int s_itemId = 101;
        public string OrderId { get; set; }
        public string FoodId { get; set; }
        public decimal OrderPrice { get; set; }
        public int OrderQuantity { get; set; }

        public Cart(string order, string food, decimal price, int quantity)
        {
            ItemId = $"ITID{s_itemId++}";
            OrderId = order;
            FoodId = food;
            OrderPrice = price;
            OrderQuantity = quantity;
        }

    }
}

