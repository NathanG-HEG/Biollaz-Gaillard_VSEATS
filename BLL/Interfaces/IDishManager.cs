using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDishManager
    {
        public int AddDish(int idRestaurant, string name, int price);
        public int SetAvailability(int idDish, bool isAvailable);
        public int SetPrice(int idDish, int price);
        public List<Dish> GetAllDishesByRestaurant(int idRestaurant);
        public Dish GetDishById(int idDish);
    }
}
