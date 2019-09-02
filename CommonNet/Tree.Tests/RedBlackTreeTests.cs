using CommonNet.Tree;
using CommonNet.Tree.Core;
using CommonNet.Tree.RedBlackCore;
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
            Assert.Throws<InvalidOperationException>(() => tree.Remove(null));
            Assert.Throws<InvalidOperationException>(() => tree.Remove(
                new PointerBackedBinaryTreeNode<RedBlackTreeNode<int>>(new RedBlackTreeNode<int>(1, true), null)));

            PointerBackedBinaryTreeNode<RedBlackTreeNode<int>>  addedNode = tree.Add(2);

            // Removing an identical node to what was added should fail
            Assert.Throws<InvalidOperationException>(() => tree.Remove(
                new PointerBackedBinaryTreeNode<RedBlackTreeNode<int>>(new RedBlackTreeNode<int>(2, false), null)));
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