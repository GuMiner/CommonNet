namespace CommonNet.Tree.RedBlackCore
{
    /// <summary>
    /// Defines the color of the node.
    /// </summary>
    /// <remarks>
    /// <see cref="Empty"/> defines a node that is in the tree for algorithmic purposes,
    /// but which isn't used.
    /// </remarks>
    public enum NodeColor
    {
        Empty = 0,
        Red = 1,
        Black = 2,
    }
}
