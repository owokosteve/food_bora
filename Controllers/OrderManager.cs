namespace foodbora
{
    class OrderManager
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

    }
}
