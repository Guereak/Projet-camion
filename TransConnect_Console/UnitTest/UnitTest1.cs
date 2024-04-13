using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TransConnect_Console;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestDjikstra()
        {
            Ville.CreateVillesFromCsv("path");
        }
    }
}
