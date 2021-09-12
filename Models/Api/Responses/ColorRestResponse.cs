using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Models.Api.Responses
{
    public class ColorRestResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Format { get; set; }
        public DateTime CreatedAt { get; set; }
        public string StatusName { get; set; }
        public int StatusId { get; set; }
    }
}
