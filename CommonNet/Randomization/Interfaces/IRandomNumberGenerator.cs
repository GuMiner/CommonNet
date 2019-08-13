namespace CommonNet.Randomization
{
    /// <summary>
    /// Defines a random number generator usable in various contexts.
    /// </summary>
    public interface IRandomNumberGenerator
        : IIntegerRandomNumberGenerator, IDoubleRandomNumberGenerator
    {
    }
}
