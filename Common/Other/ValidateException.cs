using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Other
{
    public class ValidateException : Exception
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public ValidateException(bool _isSuccess, string _Message)
        {
            isSuccess = _isSuccess;
            Message = _Message;
        }
    }
}
