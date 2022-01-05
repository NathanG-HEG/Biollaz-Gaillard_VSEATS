namespace DTO
{
    /// <summary>
    /// Class to model a dish
    /// </summary>
    public class Dish
    {
        public int IdDish { get; set; }
        public int IdRestaurant { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public bool IsAvailable { get; set; }

        public override string ToString()
        {
            return "\nIdDish: " + IdDish +
                   "\nIdRestaurant: " + IdDish +
                   "\nName: " + Name +
                   "\nPrice: " + Price+
                   "\nImage: " + Image +
                   "\nIsAvailable: " + IsAvailable;
        }

    }
}