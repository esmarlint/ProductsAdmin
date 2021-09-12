using System;
using System.Collections.Generic;

#nullable disable

namespace ProductsAdmin.Database.Models
{
    public partial class Status
    {
        public Status()
        {
            Colors = new HashSet<Color>();
            ProductPrices = new HashSet<ProductPrice>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Color> Colors { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
