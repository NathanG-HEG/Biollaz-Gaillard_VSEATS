using System;

namespace BLL.BusinessExceptions
{
    /// <summary>
    /// Exception that occurs when the user input is incorrect. This only occurs if the user finds a workaround in the MVC layer.
    /// </summary>
    public class InputSyntaxException : Exception
    {
        override 
        public string Message { get; }
        /// <summary>
        /// The exception constructor
        /// </summary>
        /// <param name="message">The message that specifies which InputSyntaxException occurred</param>
        public InputSyntaxException(string message)
        {
            Message = message;
        }
    }
}
