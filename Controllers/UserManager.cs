
using System.Text.RegularExpressions;

namespace foodbora
{
    public class UserManager
    {
       // ListManager list = new ListManager();
        List<User> users = ListManager.users;
        List<Order> orders = ListManager.orders;
        List<Cart> cart = ListManager.carts;
        public string? ID;
        public void MainMenu()
        {

            bool IsMain = true;
            do
            {
                Console.WriteLine("Welcome to FOOD BORA CAFETERIA\n Enter an Operation to Continue\n 1. User Registration\n 2. User Login\n 3. Exit ");
                int option = int.Parse(Console.ReadLine()!.Trim());
                if (option == 1)
                {
                    Registration();
                }
                else if (option == 2)
                {
                    Login();
                }
                else if (option == 3)
                {
                    IsMain = false;
                }
                else
                {
                    Console.WriteLine("Invalid User input.. Try again.");


                }
            } while (IsMain);
        }
        public void Registration()
        {
            Console.WriteLine("Enter your User name :");
            string name = Console.ReadLine()!.Trim();

            Console.WriteLine("Enter your User father's name :");
            string fathername = Console.ReadLine()!.Trim();

            Console.WriteLine("Enter your valid mobile number:");
            string Mobile = Console.ReadLine()!.Trim();

            string mailID;
            bool IsMail = true;
            do
            {
                Console.WriteLine("Enter you mail ID");
                mailID = Console.ReadLine()!.Trim();

                if (Regex.IsMatch(mailID, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    IsMail = false;
                }
                else
                {
                    IsMail = true; Console.WriteLine("Invalid email format.");
                }

            } while (IsMail);

            Console.WriteLine("Enter Gender");
            Gender gender = Enum.Parse<Gender>(Console.ReadLine()!.Trim().ToLower());
            string workStationNumber;
            bool IsWorkNumber = true;
            do
            {
                Console.WriteLine("Enter Work station Number (WS101)");
                workStationNumber = Console.ReadLine()!.Trim().ToUpper();

                if (Regex.IsMatch(workStationNumber, @"^WS1\d{2}$"))
                {
                    IsWorkNumber = false;
                }
                else { Console.WriteLine("Invalid WorkStation Number"); }
            } while (IsWorkNumber);


            Console.WriteLine("Enter the current Balance ");
            decimal balance = decimal.Parse(Console.ReadLine()!);
            User user = new User(name, fathername, gender, Mobile, mailID, workStationNumber, balance);

            users.Add(user);

            Console.WriteLine($"******You have successfully registered******\n You USER ID is {user.UserID}");
        }
        public void Login()
        {
            Console.WriteLine("To Login please enter USER ID for verification:");
            ID = Console.ReadLine()!.ToUpper().Trim();
            var verify = users.Find(x => x.UserID == ID);


            if (verify != null)
            {
                bool IsSubMenu = true;
                do
                {
                    //SUBMENU
                    Console.WriteLine("*********SUBMENU**********\n A. Show My Profile\n B. Food Order\n C. Modify Order\n D. Cancel Order\n E. Order History\n F. Wallet Recharge\n G. Show WalletBalance\n H. Exit\n Select An Option to Continue:");
                    string Choice = Console.ReadLine()!.ToUpper().Trim();
                    OrderManager order = new OrderManager();
                    if (Choice == "A") { ShowMyProfile(); }
                    else if (Choice == "B") { order.OrderFood(verify); }
                    else if (Choice == "C") { order.OrderFood(verify); }
                    else if (Choice == "D") { order.CancelOrder(verify, orders, cart); }
                    else if (Choice == "E") { order.OrderHistory(verify, orders); }
                    else if (Choice == "F") { }
                    else if (Choice == "G") { }
                    else if (Choice == "H") { IsSubMenu = false; }
                    else
                    {
                        Console.WriteLine("Invalid user input! Please Try Again");
                    }
                } while (IsSubMenu);
            }
            else
            {
                Console.WriteLine("User ID not found");
            }


        }
        public void ShowMyProfile()
        {
            Console.WriteLine($"| User ID   |   Name  | FatherName | gender   |     Mail ID     |Mobile   |  WorkStation | WalletBalance|");
            System.Console.WriteLine("***********************************************************************************************************");
            foreach (var item in users)
            {
                if (item.UserID == ID)
                {
                    Console.WriteLine($"{item.UserID,-15}{item.Name,-10}{item.FatherName,-10}{item.Gender,-15}{item.MailID,-15}  {item.Mobile,-15}{item.WorkStationNumber,-10}{item.WalletBalance,-15}");
                }

            }

        }
    }
}
    
