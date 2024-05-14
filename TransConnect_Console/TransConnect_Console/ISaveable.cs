using System;

namespace TransConnect_Console
{
    interface ISaveable
    {
        static void SaveToFile(string path) => throw new NotImplementedException();
        static void GetFromFile(string path) => throw new NotImplementedException();
    }
}
