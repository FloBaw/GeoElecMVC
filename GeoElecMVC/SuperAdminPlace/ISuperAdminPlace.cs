using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoElecMVC.SuperAdminPlace
{
    interface ISuperAdminPlace<T>
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
