using CommonNet.Tree;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CommonNet.Tests
{
    [Parallelizable]
    public class HeapTests
    {
        [Test]
        public void HeapThrowsWhenNoItems()
        {
            Heap<int> heap = new Heap<int>();
            Assert.Throws<InvalidOperationException>(() => heap.Remove());
        }

        [Test]
        public void HeapTracksItemCount()
        {
            Heap<int> heap = new Heap<int>();
            Assert.AreEqual(0, heap.NodeCount);

            heap.Add(2);
            Assert.AreEqual(1, heap.NodeCount);

            heap.Add(4);
            Assert.AreEqual(2, heap.NodeCount);

            heap.Remove();
            heap.Remove();
            Assert.AreEqual(0, heap.NodeCount);
        }

        [Test]
        public void HeapReturnsElementsInOrder()
        {
            Heap<int> heap = new Heap<int>();

            List<int> inputList = new List<int>() { 1, 7, 5, 9, 22, 4, 2, 4 };
            List<int> outputList = new List<int>() { 22, 9, 7, 5, 4, 4, 2, 1 };

            inputList.ForEach((item) => heap.Add(item));

            List<int> resultList = new List<int>();
            while (heap.NodeCount != 0)
            {
                resultList.Add(heap.Remove());
            }

            CollectionAssert.AreEqual(outputList, resultList);
        }

        [Test]
        public void HeapSupportsCustomComparer()
        {
            // The custom comparer here makes this a min-heap, not a max-heap
            Heap<int> heap = new Heap<int>(Comparer<int>.Create((first, second) => second.CompareTo(first)));

            List<int> inputList = new List<int>() { 1, 7, 5, 9, 22, 4, 2, 4 };
            List<int> outputList = new List<int>() { 1, 2, 4, 4, 5, 7, 9, 22 };

            inputList.ForEach((item) => heap.Add(item));

            List<int> resultList = new List<int>();
            while (heap.NodeCount != 0)
            {
                resultList.Add(heap.Remove());
            }

            CollectionAssert.AreEqual(outputList, resultList);
        }
    }
}