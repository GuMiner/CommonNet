namespace CommonNet.Randomization
{
    /// <summary>
    /// Defines a random number generator for double values.
    /// </summary>
    public interface IDoubleRandomNumberGenerator
    {
        /// <summary>
        /// Returns a random double greater or equal to 0.0 and less than 1.0
        /// </summary>
        /// <remarks>
        /// Mirrored from https://docs.microsoft.com/en-us/dotnet/api/system.random.nextdouble?view=netframework-4.8
        /// </remarks>
        /// <returns>A random double greater or equal to 0.0 and less than 1.0</returns>
        double NextDouble();
    }
}