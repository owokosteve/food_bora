namespace foodbora;
public  class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\n---WELOME TO FOOD BORA HOTEL---");

        do
        {
            Console.WriteLine("\n[1] Register\n[2] Login\n[3] Exit");
            Console.Write("\nChoose option: ");
            string option = Console.ReadLine()!.Trim();

            switch (option)
            {
                case "1":
                    new UserManager().Registration();
                    break;
                case "2":
                    new UserManager().Login();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again!");
                    break;

            }
        } while (true);

    }
}