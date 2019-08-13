using CommonNet.Randomization;
using CommonNet.Sequence;
using System.Collections.Generic;

namespace CommonNet.Permutation
{
    /// <summary>
    /// Performs an in place sequence randomization using the
    /// https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle on a given <see cref="IReadOnlyCollection{T}"/>
    /// </summary>
    public class FisherYatesShuffle
    {
        private readonly IIntegerRandomNumberGenerator randomNumberGenerator;

        public FisherYatesShuffle(IIntegerRandomNumberGenerator randomNumberGenerator)
        {
            this.randomNumberGenerator = randomNumberGenerator;
        }

        public void Shuffle<T>(IList<T> elements)
        {
            // We could iterate to the end, but then we shuffle the last element with itself, which is totally unnecessary.
            for (int i = 0; i < elements.Count - 1; i++)
            {
                int numbersLeft = elements.Count - i;
                int randomIndex = this.randomNumberGenerator.NextIntUnsafe(numbersLeft) + i;

                elements.SwapUnsafe(i, randomIndex);
            }
        }
    }
}
