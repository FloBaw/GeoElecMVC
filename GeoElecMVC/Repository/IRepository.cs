using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthPostMVC.Models;

namespace AuthPostMVC.Repository
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
