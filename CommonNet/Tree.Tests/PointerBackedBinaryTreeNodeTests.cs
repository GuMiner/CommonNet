using CommonNet.Tree.Core;
using NUnit.Framework;

namespace CommonNet.Tests
{
    [Parallelizable]
    public class PointerBackedBinaryTreeNodeTests
    {
        [Test]
        public void GetSiblingFindsSibling()
        {
            PointerBackedBinaryTreeNode<string> testNode = new PointerBackedBinaryTreeNode<string>("abc", null);
            testNode.Left = new PointerBackedBinaryTreeNode<string>("def", testNode);
            testNode.Right = new PointerBackedBinaryTreeNode<string>("geh", testNode);

            Assert.AreEqual(testNode.Right, testNode.Left.GetSibling());
            Assert.AreEqual(testNode.Left, testNode.Right.GetSibling());
        }

        [Test]
        public void GetSiblingFindsNoSiblingWhenNoneExists()
        {
            PointerBackedBinaryTreeNode<string> testNode = new PointerBackedBinaryTreeNode<string>("abc", null);
            testNode.Left = new PointerBackedBinaryTreeNode<string>("def", testNode);

            Assert.IsNull(testNode.GetSibling());
            Assert.IsNull(testNode.Left.GetSibling());
        }

        [Test]
        public void GetUncleFindsUncle()
        {
            PointerBackedBinaryTreeNode<string> testNode = new PointerBackedBinaryTreeNode<string>("abc", null);
            testNode.Left = new PointerBackedBinaryTreeNode<string>("def", testNode);
            testNode.Right = new PointerBackedBinaryTreeNode<string>("geh", testNode);
            testNode.Left.Left = new PointerBackedBinaryTreeNode<string>("ijk", testNode.Left);

            Assert.AreEqual(testNode.Right, testNode.Left.Left.GetUncle());

            testNode.Right.Left = new PointerBackedBinaryTreeNode<string>("lmn", testNode.Right);
            Assert.AreEqual(testNode.Left, testNode.Right.Left.GetUncle());
        }

        [Test]
        public void GetUncleFindsNoUncleWhenNoneExists()
        {
            PointerBackedBinaryTreeNode<string> testNode = new PointerBackedBinaryTreeNode<string>("abc", null);
            Assert.IsNull(testNode.GetUncle());

            testNode.Left = new PointerBackedBinaryTreeNode<string>("def", testNode);
            testNode.Left.Left = new PointerBackedBinaryTreeNode<string>("geh", testNode.Left);

            Assert.IsNull(testNode.Left.GetUncle());
            Assert.IsNull(testNode.Left.Left.GetUncle());
        }
    }
}