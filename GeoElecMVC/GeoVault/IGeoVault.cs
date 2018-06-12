using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GeoElecMVC.Models;

namespace GeoElecMVC.GeoVault
{
    public interface IGeoVault<T>
    {
        IEnumerable<T> FindAllGenFram();
        IEnumerable<T> FindAllGenFram(string searchid);
        IEnumerable<T> FindAllGenFram(DateTime datebegin, DateTime dateend);
        IEnumerable<T> FindAllGenFram(string searchid, DateTime datebegin, DateTime dateend);
        float getNrjTot(string searchid);
        float getNrjTot(string searchid, DateTime datebegin, DateTime dateend);

        IEnumerable<T> FindAllItsGenFram(string userid);
        IEnumerable<T> FindAllItsGenFram(string userid, string searchid);
        IEnumerable<T> FindAllItsGenFram(string userid, DateTime datebegin, DateTime dateend);
        IEnumerable<T> FindAllItsGenFram(string userid, string searchid, DateTime datebegin, DateTime dateend);
        float getItsNrjTot(string userid, string searchid);
        float getItsNrjTot(string userid, string searchid, DateTime datebegin, DateTime dateend);
    }
}
