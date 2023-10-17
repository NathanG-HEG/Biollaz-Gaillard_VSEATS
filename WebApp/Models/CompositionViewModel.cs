namespace WebApp.Models
{
    /// <summary>
    /// Class to manage dish information display, as well as the quantity ordered by the client.
    /// </summary>
    public class CompositionViewModel
    {
        public int IdDish { get; set; }
        public int Quantity { get; set; }
        public string DishImagePath { get; set; }
        public double DishPrice { get; set; }
        public string DishName { get; set; }
       
    }
}
