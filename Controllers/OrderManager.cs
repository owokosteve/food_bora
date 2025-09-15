namespace foodbora;

public class OrderManager
{
    List<User> users = ListManager.users;
    List<Food> foods = ListManager.foods;
    List<Order> orders = ListManager.orders;
    List<Cart> carts = ListManager.carts;

    public void OrderFood(User user)

    {
        //create a temporary local carItemtList.
        List<Cart> wishlist = new();

        //Create an Order object with current UserID, Order date as current DateTime, total price as 0, Order status as “Initiated”.
        var foodOrder = new Order(user.UserID, DateTime.Now, 0, OrderStatus.Initiated);

        string userChoice = "NO";

        bool correct = true;

        do
        {
            //show food items when user wants to order
            ListManager.Display(foods);

            //Ask the user to pick a product using FoodID, quantity required and calculate price of food.
            Console.WriteLine("Please pick a product by entering FoddID and Quantity: ");
            Console.Write("FoodID: ");
            string foodID = Console.ReadLine()!.ToUpper();

            int quantity;
            do
            {
                correct = true;
                Console.Write("Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out quantity)) { correct = false; }

            } while (!correct);

            var productPicked = foods.Find(picked => picked.FoodID == foodID);
            if (productPicked != null)
            {
                //check food quantity available
                if (quantity > productPicked.AvailableQuantity)
                {
                    Console.WriteLine($"\nSorry, only {productPicked.AvailableQuantity} available");
                }
                else if (quantity < 1)
                {
                    Console.WriteLine($"\nYou can buy at least 1 product\n");
                    correct = false;
                }
                else
                {
                    productPicked.AvailableQuantity -= quantity;

                    //create Orders object using the available data

                    var newItem = new Cart(foodOrder.OrderId, productPicked.FoodID!, productPicked.FoodPrice * quantity, quantity);

                    //add the object to local carts items list
                    wishlist.Add(newItem);

                    //ask user whether he want to pick another product
                    Console.Write("\nDo you want to pick another product? \"Yes/No\" : ");
                    do
                    {
                        userChoice = Console.ReadLine()!.ToUpper();
                        if (userChoice != "YES" && userChoice != "NO")
                            Console.Write("\nInvalid choice! Type 'Yes' or 'No': ");

                    } while (userChoice != "YES" && userChoice != "NO");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid FoodID!");
            }

        } while (userChoice == "YES");

        //proceed with purchase if food ID is valid and other conditions are met.
        if (wishlist.Any(list => list.FoodId != null)) ProceedWithPurchase(user, wishlist, foodOrder);

        // ListManager.Display(cartItems);
    }

    public void ProceedWithPurchase(User user, List<Cart> wishlist, Order order)
    {
        string userChoice;
        do
        {
            ListManager.Display(wishlist);
            Console.Write("\nProceed with purchase of the wish list? 'Yes/No' : ");
            userChoice = Console.ReadLine()!.ToUpper();
            if (userChoice == "NO")
            {
                ReturnItemsToFoodList(wishlist);
            }
            else if (userChoice == "YES")
            {
                //update the main Cart | update user balance 
                AddToCart(user, wishlist, order);
            }
            else
            {
                Console.Write("\nInvalid choice! Type 'Yes' or 'No' : ");
            }
        } while (userChoice != "YES" && userChoice != "NO");
    }

    public void AddToCart(User user, List<Cart> wishlist, Order order)
    {
        decimal totalPrice = wishlist.Sum(x => x.OrderPrice);

        if (user.WalletBalance >= totalPrice)
        {
            user.DeductAmount(totalPrice);
            carts.AddRange(wishlist);
            order.OrderStatus = OrderStatus.Ordered;
            order.TotalPrice = totalPrice;
            orders.Add(order);
            Console.WriteLine($"\nOrder placed successfully, and OrderID is {orders.Select(x => x.OrderId).Last()}\n");
        }
        else
        {
            RechargeWalletToPurchase(user, wishlist, order);
        }
    }

    public void RechargeWalletToPurchase(User user, List<Cart> localCarts, Order order)
    {
        string userInput;
        Console.WriteLine("\nInsufficient balance to purchase! Are you willing to recharge? 'Yes/No' : ");

        do
        {
            userInput = Console.ReadLine()!.ToUpper();
            if (userInput == "NO")
            {
                Console.WriteLine("\nexiting without order due to insufficient balance!");
                ReturnItemsToFoodList(localCarts);
            }
            else if (userInput == "YES")
            {
                Console.WriteLine($"Deficit : {order.TotalPrice - user.WalletBalance}");
                RechargeWallet(user);
                AddToCart(user, localCarts, order);
            }
            else
                Console.WriteLine("Invalid choice, Please Input 'Yes/No' : ");
        } while (userInput != "YES" && userInput != "NO");
    }

    public void RechargeWallet(User user)
    {
        decimal amount;
        bool correct;
        do
        {
            Console.WriteLine("\nEnter Recharge Amount: ");
            correct = decimal.TryParse(Console.ReadLine(), out amount);
            if (!correct || amount < 1)
            {
                Console.WriteLine("\nEnter a valid amount to recharge wallet (amount should be more than 0)!");
            }
        } while (!correct || amount < 1);

        user.WalletRecharge(amount);
        Console.WriteLine($"\nSuccessfully recharged {amount}/= New balance is {user.WalletBalance}/= \n");

    }

    public void ReturnItemsToFoodList(List<Cart> wishlist)
    {
        foreach (var item in foods)
        {
            foreach (var x in wishlist)
            {
                if (x.FoodId == item.FoodID)
                {
                    item.AvailableQuantity += x.OrderQuantity;
                }
            }
        }
    }


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

    //cancel food order
    public void CancelOrder(User user, List<Order> orders, List<Cart> carts)
    {
        var userOrder = ListManager.GetOrders(orders, user, true);
        if (!userOrder.Any())
        {
            Console.WriteLine("No order to cancel");
        }
        else
        {
            ListManager.Display(userOrder);
            Console.WriteLine("Enter the order ID you want to cancel:  ");
            string orderId = Console.ReadLine()!.Trim().ToUpper();

            var getOrder = userOrder.Find(x => x.OrderId == orderId);
            if (getOrder == null) { Console.WriteLine("Invalid order ID"); }
            else
            {
                var getItemList = carts.FindAll(x => x.OrderId == orderId);
                user.WalletRecharge(getOrder.TotalPrice);
                ReturnItemsToFoodList(getItemList);
                getOrder.OrderStatus = OrderStatus.Cancelled;
                Console.WriteLine("order cancelled succesfully!");

            }
        }

    }

    //order history
    public void OrderHistory(User user, List<Order> order)
    {
        var userOrder = ListManager.GetOrders(order, user, false);
        if (!userOrder.Any())
        {
            Console.WriteLine("No history currently");
        }
        else
        {
            ListManager.Display(userOrder);
        }
    }

}


