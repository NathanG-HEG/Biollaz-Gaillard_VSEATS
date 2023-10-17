namespace DTO
{
    /// <summary>
    /// Class to model a restaurant
    /// </summary>
    public class Restaurant
    {
        public int IdRestaurant { get; set; }
        public int IdArea { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PwdHash { get; set; }
        public string Salt { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }

    }
}