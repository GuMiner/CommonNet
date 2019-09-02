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

        /// <summary>
        /// Gets the number of nodes stored in the tree, assuming the tree is a complete tree.
        /// </summary>
        /// <remarks>
        /// A complete tree is a binary tree in which every level (except the last) must be completely filled.
        /// 
        /// This parameter will overestimate the number of nodes in the tree if the tree is not a complete tree.
        /// </remarks>
        public int NodeCount => this.Nodes.Count;

        /// <summary>
        /// Swaps the nodes at both indexes in an unsafe manner with *no* runtime checks performed.
        /// </summary>
        /// <param name="firstIndex">The first node index</param>
        /// <param name="secondIndex">The second node index</param>
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
            => this.ValidateBounds(2 * index + 1);

        /// <summary>
        /// Gets the right child index, returning -1 if there is no right child.
        /// </summary>
        /// <param name="index">The index of the parent</param>
        public int GetRightChildIndex(int index)
            => this.ValidateBounds(2 * index + 2);

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
