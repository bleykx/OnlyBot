using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyBot_Models.APIResponse
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
