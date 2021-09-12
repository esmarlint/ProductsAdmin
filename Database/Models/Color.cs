using System;
using System.Collections.Generic;

#nullable disable

namespace ProductsAdmin.Database.Models
{
    public partial class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Format { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Status StatusNavigation { get; set; }
    }
}
