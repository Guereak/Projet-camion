using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    interface ISaveable
    {
        void SaveToFile(string path);
        void GetFromFile(string path);
    }
}
