namespace CommonNet.Tree.Core
{
    /// <summary>
    /// Defines a binary tree node
    /// </summary>
    /// <typeparam name="T">The data stored in the tree</typeparam>
    public class PointerBackedBinaryTreeNode<T>
    {
        /// <summary>
        /// Creates a new binary tree node with the given data and parent node.
        /// </summary>
        public PointerBackedBinaryTreeNode(T data, PointerBackedBinaryTreeNode<T> parent)
        {
            this.Data = data;
            this.Parent = parent;
            this.Left = null;
            this.Right = null;
        }

        public T Data { get; }

        public PointerBackedBinaryTreeNode<T> Left { get; internal set; }

        public PointerBackedBinaryTreeNode<T> Right { get; internal set; }

        public PointerBackedBinaryTreeNode<T> Parent { get; internal set; }

        /// <summary>
        /// Returns the sibling of this node in the tree, null if there are no siblings.
        /// </summary>
        /// <remarks>
        /// Because this is a binary tree, there can only ever be one sibling.
        /// </remarks>
        public PointerBackedBinaryTreeNode<T> GetSibling()
        {
            PointerBackedBinaryTreeNode<T> sibling = null;
            if (this.Parent != null)
            {
                if (this.Parent.Left == this)
                {
                    sibling = this.Parent.Right;
                }
                else
                {
                    sibling = this.Parent.Left;
                }
            }

            return sibling;
        }

        /// <summary>
        /// Returns the uncle of this node in the tree, null if there is no uncle.
        /// </summary>
        /// <remarks>
        /// Because this is a binary tree, there can only ever be one uncle.
        /// </remarks>
        public PointerBackedBinaryTreeNode<T> GetUncle() => this.Parent?.GetSibling();
    }
}
