using System.Collections.Generic;
using DTO;

namespace DataAccessLayer.Interfaces
{
    public interface ICompositionDB
    {
        public int AddComposition(int idDish, int idOrder, int quantity);
        public List<Composition> GetCompositionsByOrder(int idOrder);
        public int DeleteCompositionsByOrder(int idOrder);
    }
}
