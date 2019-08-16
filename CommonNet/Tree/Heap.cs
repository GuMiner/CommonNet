using CommonNet.Tree.Core;
using System;
using System.Collections.Generic;

namespace CommonNet.Tree
{
    /// <summary>
    /// Defines a max-heap for storing objects.
    /// </summary>
    /// <remarks>
    /// To use this as a min-heap, set a custom comparer when constructing the heap class
    /// </remarks>
    /// <typeparam name="T">The type of objects being stored. Objects must be <see cref="IComparable{T}"/></typeparam>
    public class Heap<T> : ListBackedBinaryTree<T>
        where T: IComparable<T>
    {
        private readonly Func<T, T, int> comparisonFunction = (T first, T second) => first.CompareTo(second);

        /// <summary>
        /// Initializes a new <see cref="Heap{T}"/>.
        /// </summary>
        /// <param name="comparer">A comparer to use. If not provided, the default object comparer is used.</param>
        public Heap(IComparer<T> comparer = null)
            : base()
        {
            if (comparer != null)
            {
                comparisonFunction = comparer.Compare;
            }
        }

        /// <summary>
        /// Adds an item to the heap.
        /// </summary>
        /// <param name="item">The item to add to the heap</param>
        public void Add(T item)
        {
            this.Nodes.Add(item);

            int currentNodeIndex = this.NodeCount - 1;
            int smallerNodeIndex = -1;

            // Percolate the addition up the tree until it isn't smaller than its parent
            while ((smallerNodeIndex = this.GetSmallerParentNode(currentNodeIndex)) != -1)
            {
                this.SwapUnsafe(currentNodeIndex, smallerNodeIndex);
                currentNodeIndex = smallerNodeIndex;
            }
        }

        /// <summary>
        /// Removes the largest item in the heap.
        /// </summary>
        /// <returns>The largest item.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there are no items in the heap to remove.</exception>
        public T Remove()
        {
            if (this.NodeCount == 0)
            {
                throw new InvalidOperationException("Cannot remove the root item when there are no items in the heap!");
            }

            T rootNode = this.RootNode;

            // Pull out the root and put the child at the bottom of the tree in its place
            this.SwapUnsafe(0, this.NodeCount - 1);
            this.Nodes.RemoveAt(this.NodeCount - 1);

            int currentNodeIndex = 0;
            int largerNodeIndex = -1;

            // Percolate the root node down the tree until it isn't larger than its children
            while ((largerNodeIndex = this.GetLargerChildNode(currentNodeIndex)) != -1)
            {
                this.SwapUnsafe(currentNodeIndex, largerNodeIndex);
                currentNodeIndex = largerNodeIndex;
            }

            return rootNode;
        }

        private int GetSmallerParentNode(int currentNodeIndex)
        {
            int parentNodeIndex = this.GetParentIndex(currentNodeIndex);
            bool isParentSmaller = parentNodeIndex != -1 && comparisonFunction(this.Nodes[currentNodeIndex], this.Nodes[parentNodeIndex]) > 0;
            return isParentSmaller ? parentNodeIndex : -1; // If there is no parent or the parent is larger or equal, we return -1
        }

        private int GetLargerChildNode(int currentNodeIndex)
        {
            // This could definitely be cleaned up.
            int leftNodeIndex = this.GetLeftChildIndex(currentNodeIndex);
            int rightNodeIndex = this.GetRightChildIndex(currentNodeIndex);

            bool isLeftLarger = leftNodeIndex != -1 && comparisonFunction(this.Nodes[currentNodeIndex], this.Nodes[leftNodeIndex]) < 0;
            bool isRightLarger = rightNodeIndex != -1 && comparisonFunction(this.Nodes[currentNodeIndex], this.Nodes[rightNodeIndex]) < 0;

            int largerNodeIndex = -1; // If there are no children or all children are smaller or equal, we return -1.
            if (isLeftLarger && isRightLarger)
            {
                // Determine which of the two nodes are larger, and if so, use that node.
                if (comparisonFunction(this.Nodes[leftNodeIndex], this.Nodes[rightNodeIndex]) > 0)
                {
                    largerNodeIndex = leftNodeIndex;
                }
                else
                {
                    largerNodeIndex = rightNodeIndex;
                }
            }
            else if (isLeftLarger)
            {
                largerNodeIndex = leftNodeIndex;
            }
            else if (isRightLarger)
            {
                largerNodeIndex = rightNodeIndex;
            }

            return largerNodeIndex;
        }
    }
}
