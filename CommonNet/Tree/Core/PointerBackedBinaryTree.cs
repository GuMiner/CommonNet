using System;
using System.Collections.Generic;
using System.Linq;

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
        public PointerBackedBinaryTreeNode<T> Root { get; set; }

        /// <summary>
        /// Enumerates all nodes in the tree in-order.
        /// </summary>
        /// <returns>An in-order enumerated list of nodes.</returns>
        public IEnumerable<T> EnumerateInOrder()
        {
            if (this.Root == null)
            {
                yield break;
            }
            else
            {
                PointerBackedBinaryTreeNode<T> lastNode = null;
                PointerBackedBinaryTreeNode<T> currentNode = this.Root;
                while (currentNode != null)
                {
                    if (lastNode == currentNode.Parent && currentNode.Left != null)
                    {
                        // Iterate down the left 
                        lastNode = currentNode;
                        currentNode = currentNode.Left;
                    }
                    else if ((lastNode == currentNode.Parent && currentNode.Left == null) || lastNode == currentNode.Left)
                    {
                        // Found the last item traveling down the tree or traveling up from the left
                        yield return currentNode.Data;

                        lastNode = currentNode;
                        currentNode = (currentNode.Right != null) ? currentNode.Right : currentNode.Parent;
                    }
                    else if (lastNode == currentNode.Right)
                    {
                        lastNode = currentNode;
                        currentNode = currentNode.Parent;
                    }
                }
            }
        }

        /// <summary>
        /// Rotates the binary tree to the left around the rotate point.
        /// https://en.wikipedia.org/wiki/Tree_rotation
        /// </summary>
        /// <param name="node">The node to rotate around.</param>
        /// <remarks>The <paramref name="node"/> must exist within the tree defined by <see cref="Root"/>.</remarks>
        public void RotateLeft(PointerBackedBinaryTreeNode<T> node)
        {
            ValidateRotationNode(node, node?.Right);

            PointerBackedBinaryTreeNode<T> pivotNode = node.Right;

            // Swap the current right node with the pivot's left node
            node.Right = pivotNode.Left;
            if (node.Right != null)
            {
                node.Right.Parent = node;
            }

            pivotNode.Left = node;

            this.FixRotationParentPointers(node, pivotNode);
        }

        /// <summary>
        /// Rotates the binary tree to the right around the rotate point.
        /// https://en.wikipedia.org/wiki/Tree_rotation
        /// </summary>
        /// <param name="node">The node to rotate around.</param>
        /// <remarks>The <paramref name="node"/> must exist within the tree defined by <see cref="Root"/>.</remarks>
        public void RotateRight(PointerBackedBinaryTreeNode<T> node)
        {
            ValidateRotationNode(node, node?.Left);

            PointerBackedBinaryTreeNode<T> pivotNode = node.Left;

            // Swap the current left node with the pivot's right node
            node.Left = pivotNode.Right;
            if (node.Left != null)
            {
                node.Left.Parent = node;
            }

            pivotNode.Right = node;

            this.FixRotationParentPointers(node, pivotNode);
        }

        /// <summary>
        /// Sets the parent of <paramref name="childNode"/> to point to <paramref name="replacementNode"/>, updating <see cref="Root"/> as needed.
        /// Does not validate the given nodes are part of the tree.
        /// </summary>
        /// <param name="childNode">The child node to update.</param>
        /// <param name="replacementNode">The node to replace thie child node with.</param>
        public void SetParentPointer(PointerBackedBinaryTreeNode<T> childNode, PointerBackedBinaryTreeNode<T> replacementNode)
        {
            if (childNode == null)
            {
                throw new ArgumentNullException(nameof(childNode), "Cannot update pointers -- the child node cannot be null");
            }

            if (childNode.Parent != null)
            {
                if (childNode.Parent.Left == childNode)
                {
                    childNode.Parent.Left = replacementNode;
                }
                else
                {
                    childNode.Parent.Right = replacementNode;
                }
            }
            else
            {
                // This was the root node
                this.Root = replacementNode;
            }

            if (replacementNode != null)
            {
                replacementNode.Parent = childNode.Parent;
            }
        }

        /// <summary>
        /// Validates that the node to rotate and the pivot node both exist.
        /// </summary>
        private static void ValidateRotationNode(PointerBackedBinaryTreeNode<T> node, PointerBackedBinaryTreeNode<T> pivotNode)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Cannot perform tree rotation --the node to rotate around cannot be null.");
            }

            if (pivotNode == null)
            {
                throw new ArgumentNullException(nameof(pivotNode), "Cannot perform tree rotation -- the pivot child node must exist.");
            }
        }

        /// <summary>
        /// Fix parent pointers post-rotation. Used internally, so both node and pivotNode must not be null.
        /// </summary>
        /// <param name="node">The node which is now a child of <paramref name="pivotNode"/></param>
        /// <param name="pivotNode">The node which was previously a child of <paramref name="node"/></param>
        private void FixRotationParentPointers(PointerBackedBinaryTreeNode<T> node, PointerBackedBinaryTreeNode<T> pivotNode)
        {
            this.SetParentPointer(node, pivotNode);
            node.Parent = pivotNode;
        }
    }
}
