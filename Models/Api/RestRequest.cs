﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Models.Api
{
    public class PaginationParameters
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
