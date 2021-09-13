using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Models.Api.Responses
{
    public class ProductRestResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public List<PriceRestResponse> Prices { get; set; }
    }

    public class PriceRestResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public bool IsDefaultPrice { get; set; }
        public string ColorValue { get; set; }
        public string ColorName { get; set; }
        public string ColorFormat { get; set; }
        public int ColorId { get; set; }
    }
}
