﻿using BigBasketPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBasketPro.Interfaces
{
    public interface IProduct : IGeneric<Product>
    {
        public IEnumerable<Product> getProductByBrandId(int brandId);
    }
}
