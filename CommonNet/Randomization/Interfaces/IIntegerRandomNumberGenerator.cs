namespace CommonNet.Randomization
{
    /// <summary>
    /// Defines a random number generator for integer values.
    /// </summary>
    public interface IIntegerRandomNumberGenerator
    {
        /// <summary>
        /// Returns a random integer from 0 to <see cref="int.MaxValue"/>
        /// </summary>
        /// <remarks>
        /// Mirrored from https://docs.microsoft.com/en-us/dotnet/api/system.random.next?view=netframework-4.8
        /// </remarks>
        /// <returns>A random integer from 0 to <see cref="int.MaxValue"/></returns>
        int NextInt();
    }
}