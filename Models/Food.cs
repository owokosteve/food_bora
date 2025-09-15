namespace foodbora
{
    public class Food
    {
        public string? FoodID { get; set; }
        public string? FoodName { get; set; }
        public decimal FoodPrice { get; set; }
        public int AvailableQuantity { get; set; }
        static int counter = 101;

        public Food()
        {
            FoodID = $"FID{counter++}";
        }

    }
}