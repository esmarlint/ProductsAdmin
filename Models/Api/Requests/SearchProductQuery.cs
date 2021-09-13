using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Models.Request.Api
{
    public class SearchProductQuery
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
