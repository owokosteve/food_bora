namespace foodbora;

public class ListManager
{
    static string usersCSV = "CSVfiles/users.csv";
    static string ordersCSV = "CSVfiles/orders.csv";
    static string cartCSV = "CSVfiles/cart_items.csv";
    static string foodsCSV = "CSVfiles/foods.csv";

    public static List<User> users = ReadUsersFromCSV();
    public static List<Food> foods = ReadFoodFromCSV();
    public static List<Order> orders = ReadOrdersFromCSV();
    public static List<Cart> carts = ReadCartFromCSV();

    public static void CreateDirectory()
    {
        string folderPath = "CSVfiles";
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
    }
    public static void CreateCsvFiles()
    {
        if (!File.Exists(usersCSV))
            File.Create(usersCSV);
        if (!File.Exists(ordersCSV))
            File.Create(ordersCSV);
        if (!File.Exists(cartCSV))
            File.Create(cartCSV);
        if (!File.Exists(foodsCSV))
            File.Create(foodsCSV);
    }

    public static List<User> ReadUsersFromCSV()
    {
        List<User> users = new();
        var lines = File.ReadAllLines(usersCSV);

        foreach (var line in lines)
        {
            var values = line.Split(',');
            // User(string name, string fatherName, Gender gender, string mobile, string mailID,string workStationNumber, decimal balance) 
            var user = new User(values[1],values[2],Enum.Parse<Gender>(values[5]),values[3],values[4],values[6],Convert.ToDecimal(values[7])) { UserID = values[0] };
            users.Add(user);
        }
        return users;
    }

    public static List<Order> ReadOrdersFromCSV()
    {
        List<Order> orders = new();
        var lines = File.ReadAllLines(ordersCSV);

        foreach (var line in lines)
        {
            var values = line.Split(',');
            // (string userId, DateTime orderDate, decimal totalPrice, OrderStatus orderStatus)
            var order = new Order(values[1],Convert.ToDateTime(values[2]),decimal.Parse(values[3]),Enum.Parse<OrderStatus>(values[4])) { OrderId = values[0]};
            orders.Add(order);
        }
        return orders;
    }

    public static List<Cart> ReadCartFromCSV()
    {
        List<Cart> cart = new();
        var lines = File.ReadAllLines(cartCSV);

        foreach (var line in lines)
        {
            var values = line.Split(',');
            // (string order, string food, decimal price, int quantity)
            var cartItem = new Cart(values[1],values[2],decimal.Parse(values[3]),int.Parse(values[4])) { ItemId = values[0]};
            cart.Add(cartItem);
        }
        return cart;
    }

    public static List<Food> ReadFoodFromCSV()
    {
        List<Food> foods = new();
        var lines = File.ReadAllLines(foodsCSV);

        foreach (var line in lines)
        {
            var values = line.Split(',');
            var food = new Food { FoodID = values[0], FoodName = values[1], FoodPrice = decimal.Parse(values[2]), AvailableQuantity = int.Parse(values[3]) };
            foods.Add(food);
        }
        return foods;
    }

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

    public static List<Order> GetOrders(List<Order> orders, User user, bool isOrderStatus)
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
    public static void Display(List<Order> orders)
    {
        foreach (var order in orders)
        {
            Console.WriteLine("{0,-10}{1,-15}{2,-20}{3,-15}{4,-10}", order.OrderId,order.OrderDate,order.TotalPrice,order.OrderStatus,order.UserId);
        }
    }
}

