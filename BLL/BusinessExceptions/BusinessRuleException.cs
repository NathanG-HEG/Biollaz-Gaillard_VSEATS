using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessExceptions
{
    public class BusinessRuleException:Exception
    {
        override 
        public string Message { get; }
        public BusinessRuleException(string message)
        {
            Message = message;
        }
    }
}
