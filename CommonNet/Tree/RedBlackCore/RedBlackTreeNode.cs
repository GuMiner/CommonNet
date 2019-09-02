namespace CommonNet.Tree.RedBlackCore
{
    /// <summary>
    /// Defines a node in a Red-Black Tree
    /// </summary>
    public class RedBlackTreeNode<T>
    {
        public const bool Red = false;
        public const bool Black = true;

        public RedBlackTreeNode(T data, bool isBlack)
        {
            this.Data = data;
            this.IsBlack = isBlack;
        }

        public T Data { get; }

        public bool IsBlack { get; set; }
    }
}
