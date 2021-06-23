﻿using ProjectBigBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjectBigBasket.Interface
{
    public interface IProduct : IGeneric<Product>
    {
        public IEnumerable<Product> getProductByBrandId(string brandId);

    }
}
