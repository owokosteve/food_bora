namespace foodbora
{
    class OrderManager
    {
        //cancel food order
        public void CancelOrder(User user, List<Order> orders, List<Cart> carts)
        {
            var userOrder = GetOrders(orders, user, true);
            if (!userOrder.Any())
            {
                Console.WriteLine("No order to cancel");
            }
            else
            {
                Display(userOrder);
                Console.WriteLine("Enter the order ID you want to cancel:  ");
                string orderId = Console.ReadLine()!.Trim().ToUpper();

                var getOrder = userOrder.Find(x => x.OrderId == orderId);
                if (getOrder == null) { Console.WriteLine("Invalid order ID"); }
                else
                {
                    var getItemList = carts.FindAll(x => x.OrderId == orderId);
                    user.WalletRecharge(getOrder.TotalPrice);
                    ReturnItemsToFoodList(foods, getItemList);
                    getOrder.OrderStatus = OrderStatus.Cancelled;
                    Console.WriteLine("order cancelled succesfully!");

                }
            }

        }

        //order history
        public void OrderHistory(User user, List<Order> order)
        {
            var userOrder = operations.GetOrders(order, user, false);
            if (!userOrder.Any())
            {
                Console.WriteLine("No history currently");
            }
            else
            {
                Display(userOrder);
            }
        }
    }
}
