using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using DTO;

namespace BLL
{
    public class ComposeManager : IComposeManager
    {
        private DishesDB DishesDb { get; }
        private OrdersDB OrdersDb { get; }
        private CompositionDB CompositionDb { get; }
        private IConfiguration Configuration { get;}
        private Utilities Utilities { get; }

        public ComposeManager(IConfiguration configuration)
        {
            Configuration = configuration;
            Utilities = new Utilities(Configuration);
            DishesDb = new DishesDB(Configuration);
            OrdersDb = new OrdersDB(Configuration);
            CompositionDb = new CompositionDB(Configuration);
        }

        public void AddComposition(int idDish, int idOrder, int quantity)
        {
            // input checks
            if (quantity <= 0 || quantity > Utilities.MaxQuantity)
                throw new InputSyntaxException("Quantity must be a positive integer lesser than 1,000,000,000");
            if (DishesDb.GetDishById(idDish) == null)
                throw new DataBaseException("Dish " + idDish + " does not exist");
            if (OrdersDb.GetOrderById(idOrder) == null)
                throw new DataBaseException("Order " + idOrder + " does not exist");

            if (CompositionDb.AddComposition(idDish, idOrder, quantity) == 0)
            {
                //AddComposition == 0 means no row were affected
                throw new DataBaseException("Composition could not be added");
            }
        }

        public List<Composition> GetCompositionsByOrder(int idOrder)
        {
            if (OrdersDb.GetOrderById(idOrder) == null)
            {
                throw new DataBaseException("Order " + idOrder + " does not exist");
            }
            return CompositionDb.GetCompositionsByOrder(idOrder);
        }

    }
}
