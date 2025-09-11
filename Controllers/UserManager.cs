// rEGISTER
// lOGIN
// Show profile
using System.Text.RegularExpressions;



namespace foodbora
{
    public class UserManager
    {
          ListManager list = new ListManager();
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

            Console.WriteLine("Enter Work station Number (WS101)");
            string workStationNumber = Console.ReadLine()!.Trim().ToUpper();

            Console.WriteLine("Enter the current Balance ");
            decimal balance = decimal.Parse(Console.ReadLine()!);
            User user = new User(name, fathername, gender, Mobile, mailID, workStationNumber, balance);
          
            list.AddUser(user);

            Console.WriteLine($"******You have successfully registered******\n You USER ID is {user.UserID}");
        }
        public void Login()
        {
            Console.WriteLine("To Login please enter USER ID for verification:");
            string ID = Console.ReadLine()!.ToUpper().Trim();
          
            foreach (var Id in list.users)
            {
                if (Id.UserID == ID)
                {
                    bool IsSubMenu = true;
                    do
                    {
                        //SUBMENU
                        Console.WriteLine("*********SUBMENU**********\n A. Show My Profile\n B. Food Order\n C. Modify Order\n D. Cancel Order\n E. Order History\n F. Wallet Recharge\n G. Show WalletBalance\n H. Exit\n Select An Option to Continue:");
                        string Choice = Console.ReadLine()!.ToUpper().Trim();
                        if (Choice == "A") { }
                        else if (Choice == "B") { }
                        else if (Choice == "C") { }
                        else if (Choice == "D") { }
                        else if (Choice == "E") { }
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
        }
     
    }
}
