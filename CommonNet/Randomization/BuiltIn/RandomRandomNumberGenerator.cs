using System;

namespace CommonNet.Randomization
{
    /// <summary>
    /// Defines an implementation of <see cref="IRandomNumberGenerator"/> which uses <see cref="Random"/>
    /// </summary>
    /// <remarks>
    /// <see cref="Random"/> is not thread safe. This generator is not thread safe either.
    /// </remarks>
    public class RandomRandomNumberGenerator : IRandomNumberGenerator
    {
        protected Random random;

        /// <summary>
        /// Creates a new <see cref="RandomRandomNumberGenerator"/>
        /// </summary>
        /// <param name="seed">The seed to use for random number generation</param>
        public RandomRandomNumberGenerator(int seed = 42)
        {
            this.random = new Random(seed);
        }

        /// <inheritdoc />
        public int NextInt()
            => this.random.Next();

        /// <inheritdoc />
        public double NextDouble()
            => this.random.NextDouble();
    }
}
