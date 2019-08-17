using CommonNet.Tree.Core;
using CommonNet.Tree.RedBlackCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonNet.Tree
{
    // TODO: Consider double-generic redesign where the first is the data, the second the sorting value.
    public class RedBlackTree<T> : ListBackedBinaryTree<RedBlackTreeNode<T>>
        where T : IComparable<T>
    {
        // TODO: Deduplicate with Heap once a third usage of this exists.
        private readonly Func<T, T, int> comparisonFunction = (T first, T second) => first.CompareTo(second);

        /// <summary>
        /// Initializes a new <see cref="Heap{T}"/>.
        /// </summary>
        /// <param name="comparer">A comparer to use. If not provided, the default object comparer is used.</param>
        public RedBlackTree(IComparer<T> comparer = null)
            : base()
        {
            if (comparer != null)
            {
                comparisonFunction = comparer.Compare;
            }

            this.NodeCount = 0;
        }

        public void Add(T data)
        {
            // TODO
            ++this.NodeCount;
        }

        public void Remove(int nodeIndex)
        {
            // TODO
            --this.NodeCount;
        }

        /// <summary>
        /// Enumerates the tree in level-order.
        /// </summary>
        /// <remarks>
        /// This enumeration will return the elements of the tree in sorted order.
        /// </remarks>
        public IEnumerable<T> Enumerate()
            => this.NodeCount == 0 ? Enumerable.Empty<T>() : this.EnumerateNodeAndChildren(0);

        /// <summary>
        /// Gets the number of nodes stored in the tree.
        /// </summary>
        public new int NodeCount { get; private set; }

        private IEnumerable<T> EnumerateNodeAndChildren(int nodeIndex)
        {
            // This is actually rather inefficient and will create lots of iterators.
            RedBlackTreeNode<T> node = this.Nodes[nodeIndex];
            if (node.Color != NodeColor.Empty)
            {
                foreach (T innerNode in this.EnumerateNodeAndChildren(this.GetLeftChildIndex(nodeIndex)))
                {
                    yield return innerNode;
                }

                yield return this.Nodes[nodeIndex].Data;

                foreach (T innerNode in this.EnumerateNodeAndChildren(this.GetRightChildIndex(nodeIndex)))
                {
                    yield return innerNode;
                }
            }
        }
    }
}
