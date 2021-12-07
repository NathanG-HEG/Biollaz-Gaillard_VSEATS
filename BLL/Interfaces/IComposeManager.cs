using DataAccessLayer;
using System.Collections.Generic;
using DTO;

namespace BLL
{
    public interface IComposeManager
    {

        public void AddComposition(int idDish, int idOrder, int quantity);
        public List<Composition> GetCompositionsByOrder(int idOrder);
        public void DeleteComposition(int idComposition);


    }
}