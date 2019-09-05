using CommonNet.Tree.Core;
using NUnit.Framework;
using System;
using System.Linq;

namespace CommonNet.Tests
{
    [Parallelizable]
    public class PointerBackedBinaryTreeTests
    {
        [Test]
        public void EnumerateInOrderHandlesEmptyTree()
        {
            Assert.AreEqual(0, new PointerBackedBinaryTree<int>().EnumerateInOrder().Count());
        }

        [Test]
        public void EnumerateInOrderHandlesTinyTree()
        {
            PointerBackedBinaryTree<int> testTree = new PointerBackedBinaryTree<int>();
            testTree.Root = new PointerBackedBinaryTreeNode<int>(1, null);
            testTree.Root.Left = new PointerBackedBinaryTreeNode<int>(2, testTree.Root);
            testTree.Root.Right = new PointerBackedBinaryTreeNode<int>(3, testTree.Root);

            CollectionAssert.AreEqual(new int[]{ 2, 1, 3 }, testTree.EnumerateInOrder());
        }

        /// <summary>
        /// 1        
        ///  \       
        ///    3     
        ///   / \    
        ///  2   5   
        ///     / \  
        ///    4   6 
        /// </summary>
        [Test]
        public void EnumerateInOrderHandlesLargeTree()
        {
            PointerBackedBinaryTree<int> testTree = new PointerBackedBinaryTree<int>();
            testTree.Root = new PointerBackedBinaryTreeNode<int>(1, null);
            testTree.Root.Right = new PointerBackedBinaryTreeNode<int>(3, testTree.Root);
            testTree.Root.Right.Left = new PointerBackedBinaryTreeNode<int>(2, testTree.Root.Right);
            testTree.Root.Right.Right = new PointerBackedBinaryTreeNode<int>(5, testTree.Root.Right);
            testTree.Root.Right.Right.Left = new PointerBackedBinaryTreeNode<int>(4, testTree.Root.Right.Right);
            testTree.Root.Right.Right.Right = new PointerBackedBinaryTreeNode<int>(6, testTree.Root.Right.Right);

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6 }, testTree.EnumerateInOrder());
        }

        [Test]
        public void RotateLeftValidatesArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new PointerBackedBinaryTree<int>().RotateLeft(null));

            // FYI this won't throw, even though the node isn't part of the tree in 'Root'
            // This is deliberate, such that if that check is added in the future, tests are also updated.
            Assert.Throws<ArgumentNullException>(() => new PointerBackedBinaryTree<int>().RotateLeft(
                new PointerBackedBinaryTreeNode<int>(1, null)));
        }

        [Test]
        public void RotateRightValidatesArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new PointerBackedBinaryTree<int>().RotateRight(null));

            Assert.Throws<ArgumentNullException>(() => new PointerBackedBinaryTree<int>().RotateRight(
                new PointerBackedBinaryTreeNode<int>(1, null)));
        }

        /// <summary>
        /// 1           1
        ///  \           \
        ///    2     ->   4
        ///   / \        /  \
        ///  3   4      2    6
        ///     / \    / \
        ///    5   6  3   5
        /// </summary>
        /// <remarks>
        /// Tree rotation keeps in-order traversal (1,3,2,5,4,6) the same.
        /// </remarks>
        [Test]
        public void RotateLeftRotatesNode()
        {
            PointerBackedBinaryTree<int> testTree = new PointerBackedBinaryTree<int>();
            testTree.Root = new PointerBackedBinaryTreeNode<int>(1, null);
            testTree.Root.Right = new PointerBackedBinaryTreeNode<int>(2, testTree.Root);
            testTree.Root.Right.Left = new PointerBackedBinaryTreeNode<int>(3, testTree.Root.Right);
            testTree.Root.Right.Right = new PointerBackedBinaryTreeNode<int>(4, testTree.Root.Right);
            testTree.Root.Right.Right.Left = new PointerBackedBinaryTreeNode<int>(5, testTree.Root.Right.Right);
            testTree.Root.Right.Right.Right = new PointerBackedBinaryTreeNode<int>(6, testTree.Root.Right.Right);

            testTree.RotateLeft(testTree.Root.Right);

            // Validate the tree structure
            Assert.AreEqual(1, testTree.Root.Data);
            Assert.AreEqual(4, testTree.Root.Right.Data);
            Assert.AreEqual(2, testTree.Root.Right.Left.Data);
            Assert.AreEqual(6, testTree.Root.Right.Right.Data);
            Assert.AreEqual(3, testTree.Root.Right.Left.Left.Data);
            Assert.AreEqual(5, testTree.Root.Right.Left.Right.Data);
        }

        /// <summary>
        /// 1           1
        ///  \           \
        ///    2     ->   3
        ///   / \          \
        ///  3   4          2
        ///     / \          \
        ///    5   6          4
        ///                  / \
        ///                 5   6
        /// </summary>
        [Test]
        public void RotateRightRotatesNode()
        {
            PointerBackedBinaryTree<int> testTree = new PointerBackedBinaryTree<int>();
            testTree.Root = new PointerBackedBinaryTreeNode<int>(1, null);
            testTree.Root.Right = new PointerBackedBinaryTreeNode<int>(2, testTree.Root);
            testTree.Root.Right.Left = new PointerBackedBinaryTreeNode<int>(3, testTree.Root.Right);
            testTree.Root.Right.Right = new PointerBackedBinaryTreeNode<int>(4, testTree.Root.Right);
            testTree.Root.Right.Right.Left = new PointerBackedBinaryTreeNode<int>(5, testTree.Root.Right.Right);
            testTree.Root.Right.Right.Right = new PointerBackedBinaryTreeNode<int>(6, testTree.Root.Right.Right);

            testTree.RotateRight(testTree.Root.Right);

            // Validate the tree structure
            Assert.AreEqual(1, testTree.Root.Data);
            Assert.AreEqual(3, testTree.Root.Right.Data);
            Assert.AreEqual(2, testTree.Root.Right.Right.Data);
            Assert.AreEqual(4, testTree.Root.Right.Right.Right.Data);
            Assert.AreEqual(5, testTree.Root.Right.Right.Right.Left.Data);
            Assert.AreEqual(6, testTree.Root.Right.Right.Right.Right.Data);
        }

        [Test]
        public void RotateLeftRotatesRootNode()
        {
            PointerBackedBinaryTree<int> testTree = new PointerBackedBinaryTree<int>();
            testTree.Root = new PointerBackedBinaryTreeNode<int>(1, null);
            testTree.Root.Right = new PointerBackedBinaryTreeNode<int>(2, testTree.Root);

            testTree.RotateLeft(testTree.Root);

            // Validate the entire tree structure after rotation, including internal pointers.
            Assert.AreEqual(2, testTree.Root.Data);
            Assert.IsNull(testTree.Root.Parent);
            Assert.IsNull(testTree.Root.Right);
            Assert.AreEqual(1, testTree.Root.Left.Data);
            Assert.AreEqual(testTree.Root, testTree.Root.Left.Parent);
            Assert.IsNull(testTree.Root.Left.Right);
            Assert.IsNull(testTree.Root.Left.Left);
        }

        [Test]
        public void RotateRightRotatesRootNode()
        {
            PointerBackedBinaryTree<int> testTree = new PointerBackedBinaryTree<int>();
            testTree.Root = new PointerBackedBinaryTreeNode<int>(1, null);
            testTree.Root.Left = new PointerBackedBinaryTreeNode<int>(2, testTree.Root);

            testTree.RotateRight(testTree.Root);

            // Validate the entire tree structure after rotation, including internal pointers.
            Assert.AreEqual(2, testTree.Root.Data);
            Assert.IsNull(testTree.Root.Parent);
            Assert.IsNull(testTree.Root.Left);
            Assert.AreEqual(1, testTree.Root.Right.Data);
            Assert.AreEqual(testTree.Root, testTree.Root.Right.Parent);
            Assert.IsNull(testTree.Root.Right.Right);
            Assert.IsNull(testTree.Root.Right.Left);
        }
    }
}