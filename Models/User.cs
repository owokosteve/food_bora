public class PersonalDetails(string name, string fatherName, Gender gender, string mobile, string mailID)
{
    public string Name { get; set; } = name;
    public string FatherName { get; set; } = fatherName;
    public Gender Gender { get; set; } = gender;
    public string Mobile { get; set; } = mobile;
    public string MailID { get; set; } = mailID;
}

public enum Gender{male,female,transgender}
interface ITransaction
{
    decimal WalletBalance { get;  }
    decimal WalletRecharge(decimal amount);
    decimal DeductAmount(decimal amount);
}
public class User(string workStationNumber, decimal balance, string name, string fatherName, Gender gender, string mobile, string mailID) : PersonalDetails(name, fatherName, gender, mobile, mailID), ITransaction

{
    private static int counter = 1001;
    public string UserID { get; set; } = $"SF{counter}++";
    public string WorkStationNumber { get; set; } = workStationNumber;
    public decimal _balance = balance;
    public decimal WalletBalance { get { return _balance; } }
    public decimal WalletRecharge(decimal amount)
    {
        return _balance += amount;
     }
    public decimal DeductAmount(decimal amount)
    {
                return _balance -= amount;

    }
}