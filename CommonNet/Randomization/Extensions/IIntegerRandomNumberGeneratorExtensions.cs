using System;

namespace CommonNet.Randomization
{
    /// <summary>
    /// Defines extensions to simplify usage of <see cref="IIntegerRandomNumberGenerator"/>
    /// </summary>
    public static class IIntegerRandomNumberGeneratorExtensions
    {
        /// <summary>
        /// Returns a random number value from 0 (inclusive) to range (exclusive)
        /// </summary>
        /// <param name="randomNumberGenerator">The random number generator to use</param>
        /// <param name="range">The max range (exclusive)</param>
        /// <returns>A random number value from 0 (inclusive) to range (exclusive)</returns>
        /// <exception cref="ArgumentOutOfRangeException">If range is 0 or less</exception>
        public static int NextInt(this IIntegerRandomNumberGenerator randomNumberGenerator, int range)
            => range <= 0 ?
                throw new ArgumentOutOfRangeException(nameof(range), $"{nameof(NextInt)}'s range is less than 1: {range}.") :
                randomNumberGenerator.NextIntUnsafe(range);

        /// <summary>
        /// Returns a random number value from 0 (inclusive) to range (exclusive)
        /// </summary>
        /// <param name="randomNumberGenerator">The random number generator to use</param>
        /// <param name="range">The max range (exclusive)</param>
        /// <returns>A random number value from 0 (inclusive) to range (exclusive)</returns>
        public static int NextIntUnsafe(this IIntegerRandomNumberGenerator randomNumberGenerator, int range)
            => randomNumberGenerator.NextInt() % range;
    }
}
