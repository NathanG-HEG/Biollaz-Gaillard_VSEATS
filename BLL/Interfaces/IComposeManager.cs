﻿using System.Collections.Generic;
using DTO;

namespace BLL.Interfaces
{
    public interface IComposeManager
    {
        public void AddComposition(int idDish, int idOrder, int quantity);
        public List<Composition> GetCompositionsByOrder(int idOrder);

    }
}