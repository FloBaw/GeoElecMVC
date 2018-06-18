using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoElecMVC.SuperAdminSiteLessee
{
    public interface ISuperAdminSiteLessee<T>
    {
        void Add(T item);
        void Remove(string username);
        void Update(T item);
        IEnumerable<T> FindAll();
        T FindByUserName(string username);

        IEnumerable<T> FindAwaitingUsers();
        T FindAwaitingUserByUserName(string username);
    }
}
