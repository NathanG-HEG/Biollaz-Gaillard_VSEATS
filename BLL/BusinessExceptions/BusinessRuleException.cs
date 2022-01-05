using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessExceptions
{
    /// <summary>
    /// Exception that occurs when a user tricked the system and try to insert wrong data in the database, or does not respect the business rules of the app
    /// </summary>
    public class BusinessRuleException:Exception
    {
        override
            public string Message { get; }
        /// <summary>
        /// Constructor of the business rule exception
        /// </summary>
        /// <param name="message">The message displayed in the console when the exception is thrown or caught</param>
        public BusinessRuleException(string message)
        {
            Message = message;
        }
    }
}
