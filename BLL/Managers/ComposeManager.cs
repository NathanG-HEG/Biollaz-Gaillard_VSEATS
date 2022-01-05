using System.Collections.Generic;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL.Managers
{
    /// <summary>
    /// The manager used to create, delete and get the compositions
    /// </summary>
    public class ComposeManager : IComposeManager
    {
        private DishesDB DishesDb { get; }
        private OrdersDB OrdersDb { get; }
        private CompositionDB CompositionDb { get; }
        private IConfiguration Configuration { get;}
        private Utilities Utilities { get; }

        /// <summary>
        /// Manager constructor
        /// </summary>
        /// <param name="configuration">The configuration used to inject the manager</param>
        public ComposeManager(IConfiguration configuration)
        {
            Configuration = configuration;
            Utilities = new Utilities(Configuration);
            DishesDb = new DishesDB(Configuration);
            OrdersDb = new OrdersDB(Configuration);
            CompositionDb = new CompositionDB(Configuration);
        }

        /// <summary>
        /// Method to add a composition in the database
        /// </summary>
        /// <param name="idDish">The id of the dish</param>
        /// <param name="idOrder">The id of the related order</param>
        /// <param name="quantity">The quantity of dish in the composition</param>
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

        /// <summary>
        /// Method to get all compositions of an order
        /// </summary>
        /// <param name="idOrder">The id of the selected order</param>
        /// <returns>A list of Composition that refers to the order in parameter</returns>
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
