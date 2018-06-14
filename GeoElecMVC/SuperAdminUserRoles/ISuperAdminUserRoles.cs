using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GeoElecMVC.Models;

namespace GeoElecMVC.SuperAdminUserRoles
{
    public interface ISuperAdminUserRoles<T>
    {
        void Add(T item);
        void Remove(string username);
        void Update(T item);
        T FindByID(string id);
        IEnumerable<T> FindAll();
        T FindByUserName(string username);

        IEnumerable<T> FindAwaitingUsers();
        T FindAwaitingUserByUserName(string username);
        void RemoveAwaiting(string username);
    }
}
