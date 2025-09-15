namespace foodbora
{
    public class OrderManager
    {
        List<Food> foods = new();
        List<Order> orders = new();
        List<Cart> carts = new();

        public void ModifyOrder(User user)
        {
            //Showing Logged-In User Details
            Console.WriteLine("USER ORDER DETAILS:");
            var userOrder = orders.FindAll(x => x.UserId == user.UserID && x.OrderStatus == OrderStatus.Ordered);
            foreach (var item in userOrder)
            {
                Console.WriteLine(@$"Order ID:{item.OrderId}
                User ID:{item.UserId}
                Order Date:{item.OrderDate.ToString("dd MMMM yyyy")}
                Order Status:{item.OrderStatus}
                Total Price:{item.TotalPrice}");
            }

            //Verifying User Order ID
            Console.WriteLine("Enter Order ID to continue");

            string orderId = Console.ReadLine()!.ToUpper().Trim();
            var selectedOrder = userOrder.Find(x => x.OrderId == orderId)!;

            if (selectedOrder == null)
            {
                Console.WriteLine("Invalid Order ID");
            }
            else
            {
                //Showing  User Cart Details
                Console.WriteLine("\nCART ITEM DETAILS:");
                Console.WriteLine(new string('.', 50));

                var activeCart = carts.FindAll(x => x.OrderId == selectedOrder.OrderId);

                foreach (var item in activeCart)
                {
                    Console.WriteLine(@$"Item ID:{item.ItemId}
                    Order ID:{item.OrderId}
                    Food ID: {item.FoodId}
                    Order Price :{item.OrderPrice}
                    Order Quantity :{item.OrderQuantity}");

                    Console.WriteLine(new string('.', 50));
                }
                // Verifying Item ID
                Console.WriteLine("Enter Item ID to continue");
                string item_Id = Console.ReadLine()!.ToUpper().Trim();

                Cart? selectedItem = activeCart.Find(x => x.ItemId == item_Id);

                if (selectedItem == null)
                {
                    Console.WriteLine("Invalid Item ID");
                }
                else
                {
                    //Modifying Order
                    Food? selectedFood = foods.Find(x => x.FoodID == selectedItem.FoodId);

                    Console.WriteLine($"Modify purchase by:\n1.Increasing Order\n2.Decreasing Order");

                    string choice = Console.ReadLine()!.Trim();

                    if (choice == "1")
                    {
                        Console.WriteLine(@"Enter new quantity to continue:");
                        int increaseQuantity = int.Parse(Console.ReadLine()!);

                        if (increaseQuantity > selectedFood!.AvailableQuantity)
                        {
                            Console.WriteLine($"\nFood quantity not available.Available quantity is:{selectedFood.AvailableQuantity}");
                        }
                        else
                        {
                            //Calculating price, deducting price and decreasing stock
                            decimal newIncreasedPrice = selectedFood.FoodPrice * increaseQuantity;

                            if (newIncreasedPrice <= user.WalletBalance)
                            {
                                user.DeductAmount(newIncreasedPrice);

                                selectedOrder!.TotalPrice = selectedOrder.TotalPrice + newIncreasedPrice;
                                selectedItem.OrderQuantity += increaseQuantity;
                                selectedFood.AvailableQuantity -= increaseQuantity;

                                Console.WriteLine($"Remaining food quantity = {selectedFood.AvailableQuantity}");
                                Console.WriteLine("Order Modified Successfully");
                            }
                        }
                    }
                    else if (choice == "2")
                    {
                        //Calculating price,refunding user wallet and increasing stock
                        Console.WriteLine("Enter new quantity to continue:");
                        int newDecreaseQuantity = int.Parse(Console.ReadLine()!);

                        var newDecreasedPrice = selectedItem.OrderPrice / selectedItem.OrderQuantity * newDecreaseQuantity;

                        selectedOrder!.TotalPrice = newDecreasedPrice;
                        selectedItem.OrderQuantity = newDecreaseQuantity;
                        selectedFood!.AvailableQuantity += newDecreaseQuantity;

                        user.WalletRecharge(newDecreasedPrice);

                        Console.WriteLine($"Remaining food quantity = {selectedFood.AvailableQuantity}");
                        Console.WriteLine("Order Modified Successfully");
                    }
                }
            }
        }
    }
}
