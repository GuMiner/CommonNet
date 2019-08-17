using CommonNet.Tree;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CommonNet.Tests
{
    [Parallelizable]
    public class RedBlackTreeTests
    {
        [Test]
        public void RedBlackTreeThrowsWithInvalidRemoval()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            Assert.Throws<InvalidOperationException>(() => tree.Remove(-1));
            Assert.Throws<InvalidOperationException>(() => tree.Remove(0));

            tree.Add(2);
            Assert.Throws<InvalidOperationException>(() => tree.Remove(1));

            tree.Add(3);
            Assert.Throws<InvalidOperationException>(() => tree.Remove(2));
        }

        [Test]
        public void RedBlackTreeTracksItemCount()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            Assert.AreEqual(0, tree.NodeCount);

            tree.Add(2);
            Assert.AreEqual(1, tree.NodeCount);

            tree.Add(4);
            Assert.AreEqual(2, tree.NodeCount);

            tree.Add(1);
            Assert.AreEqual(3, tree.NodeCount);

            tree.Add(-1);
            Assert.AreEqual(4, tree.NodeCount);
        }

        [Test]
        public void RedBlackTreeHandlesArbitraryRemovals()
        {
            // TODO implement
        }


        [Test]
        public void RedBlackTreeReturnsElementsInOrder()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();

            List<int> inputList = new List<int>() { 1, 7, 5, 9, 22, 4, 2, 4 };
            List<int> outputList = new List<int>() { 1, 2, 4, 4, 5, 7, 9, 22 };

            inputList.ForEach((item) => tree.Add(item));
            CollectionAssert.AreEqual(outputList, tree.Enumerate());
        }

        [Test]
        public void RedBlackTreeSupportsCustomComparer()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>(Comparer<int>.Create((first, second) => second.CompareTo(first)));

            List<int> inputList = new List<int>() { 1, 7, 5, 9, 22, 4, 2, 4 };
            List<int> outputList = new List<int>() { 22, 9, 7, 5, 4, 4, 2, 1 };

            inputList.ForEach((item) => tree.Add(item));
            CollectionAssert.AreEqual(outputList, tree.Enumerate());
        }
    }
}