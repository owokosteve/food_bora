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

}