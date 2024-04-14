using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    interface ISaveable
    {
        static void SaveToFile(string path) => throw new NotImplementedException();
        static void GetFromFile(string path) => throw new NotImplementedException();
    }
}
