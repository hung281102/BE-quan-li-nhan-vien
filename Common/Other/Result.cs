using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Other
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public Result()
        {
            
        }
        public Result(bool _IsSuccess, string _Message, object _Data)
        {
            IsSuccess = _IsSuccess;
            Message = _Message;
            Data = _Data;
        }
    }
}
