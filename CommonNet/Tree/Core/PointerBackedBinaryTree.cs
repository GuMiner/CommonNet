namespace CommonNet.Tree.Core
{
    /// <summary>
    /// Defines a binary tree backed by a series of pointers.
    /// </summary>
    /// <typeparam name="T">The data stored in the tree</typeparam>
    public class PointerBackedBinaryTree<T>
    {
        public PointerBackedBinaryTree()
        {
            this.Root = null;
        }

        /// <summary>
        /// The root node. May be null if the tree is empty.
        /// </summary>
        public PointerBackedBinaryTreeNode<T> Root { get; protected set; }
    }
}
