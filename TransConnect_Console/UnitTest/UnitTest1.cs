﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransConnect_Console;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestDjikstra()
        {
            //Ville.CreateVillesFromCsv("path");
        }


        [TestMethod]
        public void TestListeChainee()
        {
            ListeChainee<int> listeChainee = new ListeChainee<int> { 1, 3, 5, 7, 9};

            Assert.AreEqual(listeChainee[1], 3);
            Assert.AreEqual(listeChainee.Count, 5);
            listeChainee[4] = 0;
            Assert.AreEqual(listeChainee[4], 0);
            //Console.WriteLine(listeChainee[5]);
            Assert.AreEqual(listeChainee.Sum(x => x), 16);
        }

        [TestMethod]
        public void TestSumListeChainee()
        {
            ListeChainee<int> listeChainee = new ListeChainee<int> { 1, 3, 5, 7, 9 };
            Assert.AreEqual(listeChainee.Sum(x => x), 25);

        }

        [TestMethod]
        public void TestFindAll()
        {
            ListeChainee<int> listeChainee = new ListeChainee<int> { 1, 3, 4, 6, 9 };
            ListeChainee<int> newList = listeChainee.FindAll(x => x % 2 == 1);

            Assert.AreEqual(newList.Count, 3);
            Assert.AreEqual(newList[0], 1);
            Assert.AreEqual(newList[1], 3);
            Assert.AreEqual(newList[2], 9);
        }

        [TestMethod]
        public void TestFindAll2()
        {
            ListeChainee<int> listeChainee = new ListeChainee<int> { 1, 3, 4, 6, 9 };
            ListeChainee<int> newList = listeChainee.FindAll(x => x * 2 == 1);

            Assert.AreEqual(newList.Count, 0);
        }

        [TestMethod]
        public void TestReverse()
        {
            ListeChainee<int> listeChainee = new ListeChainee<int> { 1, 2, 3, 4, 5 };

            listeChainee.Reverse();

            Assert.AreEqual(listeChainee[0], 5);
            Assert.AreEqual(listeChainee[1], 4);
            Assert.AreEqual(listeChainee[2], 3);
            Assert.AreEqual(listeChainee[3], 2);
            Assert.AreEqual(listeChainee[4], 1);
        }

        [TestMethod]
        public void TestRemoveAt1()
        {
            ListeChainee<int> listeChainee = new ListeChainee<int> { 1, 2, 3, 4, 5 };
            listeChainee.RemoveAt(3);

            Assert.AreEqual(listeChainee[0], 1);
            Assert.AreEqual(listeChainee[1], 2);
            Assert.AreEqual(listeChainee[2], 3);
            Assert.AreEqual(listeChainee[3], 5);
        }

        [TestMethod]
        public void TestRemoveAt2()
        {
            ListeChainee<int> listeChainee = new ListeChainee<int> { 1, 2, 3, 4, 5 };
            listeChainee.RemoveAt(0);

            Assert.AreEqual(listeChainee[0], 2);
            Assert.AreEqual(listeChainee[1], 3);
            Assert.AreEqual(listeChainee[2], 4);
            Assert.AreEqual(listeChainee[3], 5);
        }

        [TestMethod]
        public void TestRemoveAt3()
        {
            ListeChainee<int> listeChainee = new ListeChainee<int> { 1, 2, 3, 4, 5 };
            listeChainee.RemoveAt(4);

            Assert.AreEqual(listeChainee[0], 1);
            Assert.AreEqual(listeChainee[1], 2);
            Assert.AreEqual(listeChainee[2], 3);
            Assert.AreEqual(listeChainee[3], 4);
        }
    }
}
