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
    }
}
