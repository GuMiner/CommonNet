using CommonNet.Tree.Core;
using CommonNet.Tree.RedBlackCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonNet.Tree
{
    /// <summary>
    /// Defines a red-black tree for efficient insertion, deletion, and searching of objects.
    /// </summary>
    /// <remarks>
    /// For more information, see https://en.wikipedia.org/wiki/Red-black_tree
    /// </remarks>
    /// <typeparam name="T">The type of data stored in the tree.</typeparam>
    /// // TODO: Consider double-generic redesign where the first is the data, the second the sorting value.
    public class RedBlackTree<T> : PointerBackedBinaryTree<RedBlackTreeNode<T>>
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
        }

        /// <summary>
        /// Adds <paramref name="data"/> to the tree
        /// </summary>
        /// <param name="data">The data to add</param>
        /// <returns>The node containing the specified data</returns>
        public PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> Add(T data)
        {
            PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> newNode = null;

            if (this.Root == null)
            {
                this.Root = new PointerBackedBinaryTreeNode<RedBlackTreeNode<T>>(
                    new RedBlackTreeNode<T>(data, RedBlackTreeNode<T>.Red), null);
                newNode = this.Root;
            }
            else
            {
                newNode = this.AddRecursive(data, this.Root);
            }

            this.InsertRepair(newNode);

            return newNode;
        }

        // TODO: Add a find method, which would work better with a dual-generic design
        // TODO: Add a 'find closest' method, which would also work better with the above.

        /// <summary>
        /// Removes the specified node (the object itself) from the tree.
        /// </summary>
        /// <remarks>Duplicates according to <see cref="IComparable{T}"/> are ignored -- only the specified node is removed</remarks>
        /// <param name="node">The node to remove.</param>
        public void Remove(PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> node)
        {
            // TODO -- implement
        }

        /// <summary>
        /// Travel down the tree recursively, adding the node at the best leaf found.
        /// </summary>
        private PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> AddRecursive(T data, PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> currentNode)
        {
            PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> newNode = null;
            if (comparisonFunction(currentNode.Data.Data, data) < 0)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = new PointerBackedBinaryTreeNode<RedBlackTreeNode<T>>(
                        new RedBlackTreeNode<T>(data, RedBlackTreeNode<T>.Red), currentNode);
                    newNode = currentNode.Left;
                }
                else
                {
                    newNode = this.AddRecursive(data, currentNode.Left);
                }
            }
            else
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = new PointerBackedBinaryTreeNode<RedBlackTreeNode<T>>(
                        new RedBlackTreeNode<T>(data, RedBlackTreeNode<T>.Red), currentNode);
                    newNode = currentNode.Right;
                }
                else
                {
                    newNode = this.AddRecursive(data, currentNode.Right);
                }
            }

            return newNode;
        }

        /// <summary>
        /// Ensures the tree follows red-black properties after an insertion of the given node.
        /// </summary>
        private void InsertRepair(PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> currentNode)
        {
            if (currentNode.Parent == null)
            {
                // If this is the root node, it must be black. It will currently be red, because
                //  that is the color new nodes are added as.
                currentNode.Data.IsBlack = true;
            }
            else if (currentNode.Parent.Data.IsBlack)
            {
                // If the parent node is black, the red-black ordering isn't invalidated, so do nothing.
            }
            else if (currentNode.GetUncle()?.Data.IsBlack == false)
            {
                // There is a red uncle (and the current node is red), so we can switch then both to black with the 
                //  grandparent becoming red. This may violate #2, so we need to rerun the recursion.
                currentNode.Parent.Data.IsBlack = true;
                currentNode.GetUncle().Data.IsBlack = true;

                // Because we found an uncle, a grandparent is guaranteed to exist.
                currentNode.Parent.Parent.Data.IsBlack = false;
                InsertRepair(currentNode.Parent.Parent);
            }
            else // parent is red, uncle is black or missing, grandparent definitely exists.
            {
                // Rotate left or right and then reverse the rotation later to effectively switch nodes around
                // and satisfy red-black height and color properties
                if (currentNode == currentNode.Parent.Right && currentNode.Parent == currentNode.Parent.Parent.Left)
                {
                    this.RotateLeft(currentNode.Parent);
                    currentNode = currentNode.Left;

                    this.RotateRight(currentNode.Parent.Parent);
                }
                else if (currentNode == currentNode.Parent.Left && currentNode.Parent == currentNode.Parent.Parent.Right)
                {
                    this.RotateRight(currentNode.Parent);
                    currentNode = currentNode.Right;

                    this.RotateLeft(currentNode.Parent.Parent);
                }

                // Switching nodes requires switching colors.
                currentNode.Parent.Data.IsBlack = true;
                currentNode.Parent.Parent.Data.IsBlack = false;
            }
        }
    }
}
