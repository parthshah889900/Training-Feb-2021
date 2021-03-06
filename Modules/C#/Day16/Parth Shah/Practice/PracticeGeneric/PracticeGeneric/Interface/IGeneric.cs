﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PracticeGeneric.Interface
{
    public interface IGeneric<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);



    }
}
