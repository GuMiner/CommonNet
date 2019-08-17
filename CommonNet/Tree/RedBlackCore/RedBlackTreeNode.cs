namespace CommonNet.Tree.RedBlackCore
{
    /// <summary>
    /// Defines a node in a Red-Black Tree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedBlackTreeNode<T>
    {
        public RedBlackTreeNode(T data, NodeColor color)
        {
            this.Data = data;
            this.Color = color;
        }

        public T Data { get; }

        public NodeColor Color { get; set; }
    }
}
