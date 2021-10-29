using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessExceptions
{
    public class DataBaseException:Exception
    {
        public string Message { get; }
        public DataBaseException(string message)
        {
            Message = message;
        }
    }
}
