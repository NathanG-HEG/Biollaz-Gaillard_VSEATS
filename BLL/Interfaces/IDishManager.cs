using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDishManager
    {
        public void AddDish(int idRestaurant, string name, int price);
        public void SetAvailability(int idDish, bool isAvailable);
        public void SetPrice(int idDish, int price);
        public List<Dish> GetAllDishesByRestaurant(int idRestaurant);
        public Dish GetDishById(int idDish);
    }
}
