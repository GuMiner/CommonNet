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

        /// <summary>
        /// Attempts to find the first node with the specified data. Returns <c>null</c> if no node was found.
        /// </summary>
        /// <param name="data">The data to find.</param>
        /// <returns>The node found, null if none found.</returns>
        public PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> Find(T data)
        {
            PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> currentNode = this.Root;
            while (currentNode != null)
            {
                int comparisonResult = comparisonFunction(currentNode.Data.Data, data);
                if (comparisonResult > 0)
                {
                    currentNode = currentNode.Left;
                }
                else if (comparisonResult == 0)
                {
                    // Found the node we want!
                    break;
                }
                else
                {
                    currentNode = currentNode.Right;
                }
            }

            return currentNode;
        }

        /// <summary>
        /// Removes the specified node (the object itself) from the tree.
        /// </summary>
        /// <remarks>Duplicates according to <see cref="IComparable{T}"/> are ignored -- only the specified node is removed</remarks>
        /// <param name="node">The node to remove.</param>
        public void Remove(PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Cannot remove a node which does not exist in the tree.");
            }

            PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> childNode;
            if (node.Left == null && node.Right == null)
            {
                // Node has no children, so remove it directly.
                if (node.Parent == null)
                {
                    this.Root = null;
                    return;
                }
                else
                {
                    this.SetParentPointer(node, null);
                }
            }
            else
            {
                // Delete node, replacing with child node.
                childNode = node.Left == null ? node.Right : node.Left;
                childNode.Parent = node.Parent;
                if (node.Parent.Left == node)
                {
                    node.Parent.Left = childNode;
                }
                else
                {
                    node.Parent.Right = childNode;
                }

                // Clean up colors
                if (node.Data.IsBlack)
                {
                    // Switch red to black in the easy case.
                    if (!childNode.Data.IsBlack)
                    {
                        childNode.Data.IsBlack = true;
                    }
                    else
                    {
                        DeleteRepair(childNode);
                    }
                }
            }
        }

        private void DeleteRepair(PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> node)
        {
            if (node.Parent != null)
            {
                // If this node isn't the root, we need to...
                PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> sibling = node.GetSibling();
                if (!sibling.Data.IsBlack)
                {
                    // ... reverse colors
                    sibling.Parent.Data.IsBlack = false;
                    sibling.Data.IsBlack = true;

                    // .... and rotate around
                    if (node.Parent.Left == node)
                    {
                        this.RotateLeft(node.Parent);
                    }
                    else
                    {
                        this.RotateRight(node.Parent);
                    }
                }

                // Once we do that, we need to possibly ...
                if (sibling.Data.IsBlack && sibling.Left.Data.IsBlack && sibling.Right.Data.IsBlack)
                {
                    // ... repeat recursivly or color theparent
                    sibling.Data.IsBlack = false;
                    if (node.Parent.Data.IsBlack)
                    {
                        
                        DeleteRepair(node.Parent);
                    }
                    else
                    {
                        node.Parent.Data.IsBlack = false;
                    }
                }
                else
                {
                    // ... or perform more rotations with coloration swaps
                    if (sibling.Data.IsBlack)
                    {
                        // Force subsequent rotations to 'do the right thing'
                        if (node.Parent.Left == node && sibling.Right.Data.IsBlack && !sibling.Left.Data.IsBlack)
                        {
                            sibling.Data.IsBlack = false;
                            sibling.Left.Data.IsBlack = true;
                            this.RotateRight(sibling);
                        }
                        else if (node.Parent.Right == node && sibling.Left.Data.IsBlack && !sibling.Right.Data.IsBlack)
                        {
                            sibling.Data.IsBlack = false;
                            sibling.Right.Data.IsBlack = true;
                            this.RotateLeft(sibling);
                        }
                    }

                    sibling.Data.IsBlack = node.Parent.Data.IsBlack;
                    node.Parent.Data.IsBlack = true;

                    if (node.Parent.Left == node)
                    {
                        sibling.Right.Data.IsBlack = true;
                        this.RotateLeft(node.Parent);
                    }
                    else
                    {
                        sibling.Left.Data.IsBlack = true;
                        this.RotateRight(node.Parent);
                    }
                }
            }
            else
            {
                this.Root = node;
            }
        }

        /// <summary>
        /// Travel down the tree recursively, adding the node at the best leaf found.
        /// </summary>
        private PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> AddRecursive(T data, PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> currentNode)
        {
            PointerBackedBinaryTreeNode<RedBlackTreeNode<T>> newNode = null;
            if (comparisonFunction(currentNode.Data.Data, data) > 0)
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

                    currentNode.Parent.Data.IsBlack = true;
                    currentNode.Parent.Parent.Data.IsBlack = false;

                    this.RotateRight(currentNode.Parent.Parent);
                }
                else if (currentNode == currentNode.Parent.Left && currentNode.Parent == currentNode.Parent.Parent.Right)
                {
                    this.RotateRight(currentNode.Parent);
                    currentNode = currentNode.Right;

                    currentNode.Parent.Data.IsBlack = true;
                    currentNode.Parent.Parent.Data.IsBlack = false;

                    this.RotateLeft(currentNode.Parent.Parent);
                }
            }
        }
    }
}
