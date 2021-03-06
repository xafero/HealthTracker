﻿using System;
using System.Collections.Generic;

namespace HealthTracker.Data
{
    public interface IRepository<T> where T : class
    {
        T Get(Guid id);

        void Save(T person);

        void Update(T person);

        void Delete(T person);

        long RowCount();

        IList<T> List();
    }
}