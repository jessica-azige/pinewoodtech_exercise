using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Common.Helpers
{
    public class APIValidation
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorDetail { get; set; }

        public APIValidation(HttpStatusCode statusCode, string errorDetail)
        {
            StatusCode = statusCode;  
            ErrorDetail = errorDetail;
        }
    }
}
