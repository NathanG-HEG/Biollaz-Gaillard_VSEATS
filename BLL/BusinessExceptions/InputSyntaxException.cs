﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessExceptions
{
    public class InputSyntaxException : Exception
    {
        public string Message { get; }
        public InputSyntaxException(string message)
        {
            Message = message;
        }
    }
}