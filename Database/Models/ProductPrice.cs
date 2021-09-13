using System;
using System.Collections.Generic;

#nullable disable

namespace ProductsAdmin.Database.Models
{
    public partial class ProductPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public bool IsDefaultPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }
        public virtual Status StatusNavigation { get; set; }
    }
}
