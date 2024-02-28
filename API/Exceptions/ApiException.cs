using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Exceptions
{
    public class ApiException
    {
        public ApiException(int statusCode,string message, string details)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Details = details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}