﻿using ProjectBigBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBigBasket.Interface
{
    public interface IBrand : IGeneric<Brand>
    {
        public IEnumerable<Brand> getBrandByCatId(string catId);

    }
}