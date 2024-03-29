﻿using System.Collections.Generic;
using DTO;

namespace DataAccessLayer.Interfaces
{
    public interface IDishesDB
    {
        public int AddDish(int idRestaurant, string name, int price);
        public int SetAvailability(int idDish, bool isAvailable);
        public int SetPrice(int idDish, int price);
        public List<Dish> GetAllDishesByRestaurant(int idRestaurant);
        public Dish GetDishById(int idDish);
    }
}
