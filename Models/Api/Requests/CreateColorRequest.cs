using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Models.Api.Requests
{
    public class CreateColorRequest
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Format { get; set; }
        public int StatusId { get; set; } 
    }
}
