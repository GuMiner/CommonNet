using CommonNet.Tree;
using CommonNet.Tree.Core;
using CommonNet.Tree.RedBlackCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonNet.Tests
{
    [Parallelizable]
    public class RedBlackTreeTests
    {
        [Test]
        public void RedBlackTreeThrowsWithInvalidRemoval()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            Assert.Throws<ArgumentNullException>(() => tree.Remove(null));
        }

        [Test]
        public void RedBlackTreeRemovesItemsAppropriately()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();

            List<int> inputList = new List<int>() { 1, 7, 5, 9, 22, 4, 2, 4 };

            inputList.ForEach((item) => tree.Add(item));

            tree.Remove(tree.Find(1));
            CollectionAssert.AreEqual(new List<int>() { 2, 4, 4, 5, 7, 9, 22 }, tree.EnumerateInOrder().Select(item => item.Data));

            tree.Remove(tree.Find(4));
            CollectionAssert.AreEqual(new List<int>() { 2, 4, 5, 7, 9, 22 }, tree.EnumerateInOrder().Select(item => item.Data));

            tree.Remove(tree.Find(4));
            CollectionAssert.AreEqual(new List<int>() { 2, 5, 7, 9, 22 }, tree.EnumerateInOrder().Select(item => item.Data));

            tree.Remove(tree.Find(22));
            CollectionAssert.AreEqual(new List<int>() { 2, 5, 7, 9}, tree.EnumerateInOrder().Select(item => item.Data));

            tree.Remove(tree.Find(7));
            CollectionAssert.AreEqual(new List<int>() { 2, 5, 9 }, tree.EnumerateInOrder().Select(item => item.Data));

            tree.Remove(tree.Find(2));
            CollectionAssert.AreEqual(new List<int>() { 5, 9 }, tree.EnumerateInOrder().Select(item => item.Data));

            tree.Remove(tree.Find(9));
            CollectionAssert.AreEqual(new List<int>() { 5 }, tree.EnumerateInOrder().Select(item => item.Data));

            tree.Remove(tree.Find(5));
            CollectionAssert.AreEqual(new List<int>() { }, tree.EnumerateInOrder().Select(item => item.Data));

            // Verify we can add everything back again.
            inputList.ForEach((item) => tree.Add(item));
            CollectionAssert.AreEqual(new List<int>() { 1, 2, 4, 4, 5, 7, 9, 22 }, tree.EnumerateInOrder().Select(item => item.Data));
        }

        [Test]
        public void RedBlackTreeFindsItems()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();

            List<int> inputList = new List<int>() { 1, 7, 5, 9, 22, 4, 2, 4 };

            inputList.ForEach((item) => tree.Add(item));

            Assert.AreEqual(1, tree.Find(1).Data.Data);
            Assert.AreEqual(22, tree.Find(22).Data.Data);
            Assert.AreEqual(4, tree.Find(4).Data.Data);
            Assert.AreEqual(9, tree.Find(9).Data.Data);

            Assert.IsNull(tree.Find(0));
            Assert.IsNull(tree.Find(-1));
            Assert.IsNull(tree.Find(100));
        }

        [Test]
        public void RedBlackTreeReturnsElementsInOrder()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();

            List<int> inputList = new List<int>() { 1, 7, 5, 9, 22, 4, 2, 4 };
            List<int> outputList = new List<int>() { 1, 2, 4, 4, 5, 7, 9, 22 };

            inputList.ForEach((item) => tree.Add(item));

            CollectionAssert.AreEqual(outputList, tree.EnumerateInOrder().Select(item => item.Data));
        }

        [Test]
        public void RedBlackTreeSupportsCustomComparer()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>(Comparer<int>.Create((first, second) => second.CompareTo(first)));

            List<int> inputList = new List<int>() { 1, 7, 5, 9, 22, 4, 2, 4 };
            List<int> outputList = new List<int>() { 22, 9, 7, 5, 4, 4, 2, 1 };

            inputList.ForEach((item) => tree.Add(item));
            CollectionAssert.AreEqual(outputList, tree.EnumerateInOrder().Select(item => item.Data));
        }
    }
}