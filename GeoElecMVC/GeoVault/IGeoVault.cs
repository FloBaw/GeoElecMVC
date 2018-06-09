using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GeoElecMVC.Models;

namespace GeoElecMVC.GeoVault
{
    public interface IGeoVault<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> getAllGenFram(string searchid);
        IEnumerable<T> getAllGenFram(DateTime datebegin, DateTime dateend);
        IEnumerable<T> getAllGenFram(string searchid, DateTime datebegin, DateTime dateend);
        float getNrjTot(string searchid, DateTime datebegin, DateTime dateend);
    }
}
