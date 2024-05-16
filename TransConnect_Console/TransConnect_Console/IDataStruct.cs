using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    internal interface IDataStruct<T>
    {
        void ForEach(Action<T> a);
        ListeChainee<T> FindAll(Predicate<T> p);
    }
}
