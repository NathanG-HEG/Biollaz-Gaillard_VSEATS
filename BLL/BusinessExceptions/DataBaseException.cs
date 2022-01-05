using System;

namespace BLL.BusinessExceptions
{
    /// <summary>
    /// Exception that occurs when an error occurred while accessing the database
    /// </summary>
    public class DataBaseException:Exception
    {
        override 
        public string Message { get; }
        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="message">The message that the databases returned when the error occurred</param>
        public DataBaseException(string message)
        {
            Message = message;
        }
    }
}
