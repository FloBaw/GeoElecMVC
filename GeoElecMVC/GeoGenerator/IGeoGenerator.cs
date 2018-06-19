using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoElecMVC.GeoGenerator
{
    public interface IGeoGenerator<T>
    {
        void Add(T item);
        void Remove(string generatorid);

        IEnumerable<T> FindAllGen();
        T FindByGenerator(string generatorid);
        IEnumerable<T> FindAwaitingGen();
        T FindByAwaitingGenerator(string generatorid);

        void UpdateGen(T item);
        void UpdateGenClient(string generatorid, int clientid);
        void UpdateGenLessee(string generatorid, int lesseeid);
        void UpdateGenPlace(string generatorid, int placeid);
    }
}
