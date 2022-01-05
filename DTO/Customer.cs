/// <summary>
/// Class to model a customer.
/// </summary>
public class Customer
{
    public int IdCustomer { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PwdHash { get; set; }
    public string Salt { get; set; }

}