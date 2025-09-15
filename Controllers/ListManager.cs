namespace foodbora;

public class ListManager
{
    List<User> users = new();
    List<Food> foods = new();
    List<Order> orders = new();
    List<Cart> carts = new();

    public  void AddUser(User user) => users.Add(user);
    public void AddFood(Food food) => foods.Add(food);
    public void AddOrder(Order order) => orders.Add(order);
    public void AddCart(Cart cart) => carts.Add(cart);
}

