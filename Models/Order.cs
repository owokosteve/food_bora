namespace foodbora
{
    public enum OrderStatus
    {
        Initiated = 0,
        Ordered = 1,
        Cancelled = 2
    }
    public class Order
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        private static int counter = 1001;
        public Order(string userId, DateTime orderDate, decimal totalPrice, OrderStatus orderStatus)
        {
            OrderId = $"OID{counter++}";
            UserId = userId;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            OrderStatus = orderStatus;
        }
    }
}
