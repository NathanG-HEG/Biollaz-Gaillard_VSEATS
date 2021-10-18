using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Composition
    {
        public int ID_Dish { get; set; }
        public int ID_order { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return "ID_dish: " + ID_Dish +
                   "\nID_order: " + ID_order +
                   "\nQuantity: " + Quantity;
        }

    }
}
