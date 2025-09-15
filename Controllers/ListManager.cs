namespace foodbora;

public class ListManager
{
    public static List<User> users = new();
    public static List<Food> foods = new();
    public static List<Order> orders = new();
    public static List<Cart> carts = new();

    public  void AddUser(User user) => users.Add(user);
    public void AddFood(Food food) => foods.Add(food);
    public void AddOrder(Order order) => orders.Add(order);
    public void AddCart(Cart cart) => carts.Add(cart);

    public static User GetUser(string user_id)
    {
        User user = users.Find(user => user.UserID == user_id);
        return user;
    }
    public static void Display(List<Food> foods)
    {
        foreach (var food in foods)
        {
            Console.WriteLine($"{food.FoodID,-10} {food.FoodName,-15} {food.FoodPrice,-10} {food.AvailableQuantity,-10}");
        }
    }

    public List<Order> GetOrders(List<Order> orders, User user, bool isOrderStatus)
    {
        if (isOrderStatus)
        {
            return orders.FindAll(o => o.UserId == user.UserID && o.OrderStatus == OrderStatus.Ordered);
        }
        else
        {
            return orders.FindAll(o => o.UserId == user.UserID);
        }
    }

    public static void Display(List<Cart> cartItems)
    {
        foreach (var cart in cartItems)
        {
            Console.WriteLine("{0,-10}{1,-15}{2,-20}{3,-15}{4,-10}", cart.FoodId, cart.ItemId, cart.OrderId, cart.OrderQuantity, cart.OrderPrice);
        }
    }
}

