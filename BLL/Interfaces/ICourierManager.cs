﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICourierManager
    {
        public void AddCourier(int idArea, string firstName, string lastName, string emailAddress, string password);
        public List<Courier> GetAllCouriersByArea(int idArea);
        public Courier GetCourierByLogin(string emailAddress, string password);
        public Courier GetCourierById(int idCourier);
    }
}
