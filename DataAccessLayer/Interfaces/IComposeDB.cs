using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DataAccessLayer.Interfaces
{
    public interface ICompositionDB
    {
        public int AddComposition(int idDish, int idOrder, int quantity);
        public List<Composition> GetCompositionsByOrder(int idOrder);
        public int DeleteComposition(int idComposition);
        public int DeleteCompositionByOrder(int idOrder);
    }
}
