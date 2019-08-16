using System.Collections.Generic;

namespace CommonNet.Tree.Core
{
    /// <summary>
    /// Defines a binary tree backed inline by a list for efficiency.
    /// </summary>
    /// <typeparam name="T">The data stored in the tree</typeparam>
    public class ListBackedBinaryTree<T>
    {
        public ListBackedBinaryTree()
        {
            this.Nodes = new List<T>();
        }

        /// <summary>
        /// The list of nodes stored in the tree
        /// </summary>
        /// <remarks>
        /// Nodes are stored linearly in level order. This significantly simplifies looking up children nodes.
        /// </remarks>
        protected List<T> Nodes { get; }

        public int NodeCount => this.Nodes.Count;

        // TODO -- Determine what to do if the node count is 0.
        public T RootNode => this.Nodes[0];

        public T LastNode => this.Nodes[NodeCount - 1];

        public void SwapUnsafe(int firstIndex, int secondIndex)
        {
            T temp = this.Nodes[firstIndex];
            this.Nodes[firstIndex] = this.Nodes[secondIndex];
            this.Nodes[secondIndex] = temp;
        }

        /// <summary>
        /// Gets the left child index, returning -1 if there is no right child.
        /// </summary>
        /// <param name="index">The index of the parent</param>
        public int GetLeftChildIndex(int index)
            => this.ValidateBounds(2 * (index + 1) - 1);

        /// <summary>
        /// Gets the right child index, returning -1 if there is no right child.
        /// </summary>
        /// <param name="index">The index of the parent</param>
        public int GetRightChildIndex(int index)
            => this.ValidateBounds(2 * (index + 1));

        /// <summary>
        /// Gets the parent index, returning -1 if there is no parent.
        /// </summary>
        /// <param name="index">The index of either child</param>
        /// <returns></returns>
        public int GetParentIndex(int index)
            => this.ValidateBounds(((index + 1) / 2) - 1);

        private int ValidateBounds(int index)
            => (index < 0 || index >= this.NodeCount) ? -1 : index;
    }
}
