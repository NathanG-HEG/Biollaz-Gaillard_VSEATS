﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRestaurantManager
    {
        public void CreateRestaurant(int idArea, string name, string emailAddress, string password);
        public void UpdateImage(string path);
        public void UpdateLogo(string path);
        public Restaurant GetRestaurantByLogin(string email, string password);
        public Restaurant GetRestaurantByName(string name);
        public List<Restaurant> GetAllRestaurantsArea(int idArea);
        public List<Restaurant> GetAllRestaurants();

    }
}