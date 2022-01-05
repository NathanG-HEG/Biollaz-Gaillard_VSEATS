
namespace DTO
{
    /// <summary>
    /// Class to model a composition. A composition is a dish times a quantity referring to an order
    /// </summary>
    public class Composition
    {
        public int ID_Dish { get; set; }
        public int ID_order { get; set; }
        public int Quantity { get; set; }


    }
}
