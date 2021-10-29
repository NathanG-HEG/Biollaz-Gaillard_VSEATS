using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class ComposeManager
    {
        private DishesDB DishesDb { get; }
        private OrdersDB OrdersDb { get; }
        private CompositionDB CompositionDb { get; }

        public ComposeManager()
        {
            DishesDb = new DishesDB();
            OrdersDb = new OrdersDB();
            CompositionDb = new CompositionDB();
        }

    }
}
