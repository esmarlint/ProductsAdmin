using System;
using System.Collections.Generic;

#nullable disable

namespace ProductsAdmin.Database.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductPrices = new HashSet<ProductPrice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Status StatusNavigation { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
