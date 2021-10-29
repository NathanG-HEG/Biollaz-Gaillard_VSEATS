using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class ComposeManager : IComposeManager
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

        public int AddComposition(int idDish, int idOrder, int quantity)
        {
            throw new NotImplementedException();
        }

        public List<Composition> GetCompositionsByOrder(int idOrder)
        {
            throw new NotImplementedException();
        }

        public void DeleteComposition(int idComposition)
        {
            throw new NotImplementedException();
        }
    }
}
