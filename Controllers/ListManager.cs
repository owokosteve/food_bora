namespace foodbora;

public class ListManager
{
    public static List<User> users = new();
    public static List<Food> foods = new();
    public static List<Order> orders = new();
    public static List<Cart> carts = new();

    public void AddUser(User user) => users.Add(user);
    public void AddFood(Food food) => foods.Add(food);
    public void AddOrder(Order order) => orders.Add(order);
    public void AddCart(Cart cart) => carts.Add(cart);
}
