using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUtilities
    {
        public bool IsEmailAddressInDatabase(string emailAddress);
        public bool IsEmailAddressCorrect(string emailAddress);
        public bool IsPasswordSyntaxCorrect(string password);

    }
}
