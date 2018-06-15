using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoElecMVC.GeoGenerator
{
    interface IGeoGenerator<T>
    {
        IEnumerable<T> FindAllGen();
        T FindByGenerator(string generatorid);
    }
}
