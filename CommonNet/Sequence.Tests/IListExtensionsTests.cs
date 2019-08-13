using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CommonNet.Sequence.Tests
{
    [Parallelizable]
    public class IListExtensionsTests
    {
        [Test]
        public void SwapTests()
        {
            List<int> testList = new List<int>() { 1, 2, 3 };
            testList.Swap(0, 0);
            testList.Swap(2, 2);
            CollectionAssert.AreEqual(new List<int>() { 1, 2, 3 }, testList);

            testList.Swap(1, 2);
            CollectionAssert.AreEqual(new List<int>() { 1, 3, 2 }, testList);

            testList.Swap(0, 2);
            CollectionAssert.AreEqual(new List<int>() { 2, 3, 1 }, testList);
        }

        [Test]
        public void SwapTestsArguments()
        {
            List<int> testList = new List<int>() { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => testList.Swap(-1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => testList.Swap(0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => testList.Swap(3, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => testList.Swap(0, 3));
        }
    }
}