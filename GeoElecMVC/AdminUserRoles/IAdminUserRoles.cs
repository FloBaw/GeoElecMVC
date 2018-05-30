using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthPostMVC.Models;

namespace AuthPostMVC.AdminUserRoles
{
    public interface IAdminUserRoles<T>
    {
        void Add(T item);
        void Remove(string username);
        void Update(T item);
        T FindByID(string id);
        IEnumerable<T> FindAll();
        T FindByUserName(string username);
    }
}
