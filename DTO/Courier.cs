/// <summary>
/// Class to model a courier. 
/// </summary>
public class Courier
{
    public int IdCourier { get; set; }
    public int IdArea { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PwdHash { get; set; }
    public string Salt { get; set; }


}