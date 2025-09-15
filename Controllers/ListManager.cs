namespace foodbora;

public class ListManager
{
    List<User> users = new();
    public List<Food> foods = new();
    List<Order> orders = new();
    public static List<Cart> carts = new();

    public void AddUser(User user) => users.Add(user);
    public void AddFood(Food food) => foods.Add(food);
    public void AddOrder(Order order) => orders.Add(order);
    public void AddCart(Cart cart) => carts.Add(cart);

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
