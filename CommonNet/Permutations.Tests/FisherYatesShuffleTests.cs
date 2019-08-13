using System.Collections.Generic;
using CommonNet.Permutation;
using CommonNet.Randomization;
using NUnit.Framework;

namespace CommonNet.Permutations.Tests
{
    [Parallelizable]
    public class FisherYatesShuffleTests
    {
        [Test]
        public void ShuffleTests()
        {
            FisherYatesShuffle shuffle = new FisherYatesShuffle(new RandomRandomNumberGenerator(seed: 42));

            List<int> testList = this.CreateTestList();
            shuffle.Shuffle(testList);

            CollectionAssert.AreEqual(new List<int>() { 1, 9, 8, 6, 4 }, testList);

            shuffle.Shuffle(testList);
            CollectionAssert.AreEqual(new List<int>() { 8, 9, 4, 1, 6 }, testList);
        }

        [Test]
        public void ShuffleEdgeCaseTests()
        {
            FisherYatesShuffle shuffle = new FisherYatesShuffle(new RandomRandomNumberGenerator(seed: 42));

            List<int> testList = new List<int>();
            shuffle.Shuffle(testList);

            CollectionAssert.AreEqual(new List<int>(), testList);

            testList = new List<int>() { 42 };
            shuffle.Shuffle(testList);
            CollectionAssert.AreEqual(new List<int>() { 42 }, testList);
        }

        private List<int> CreateTestList()
            => new List<int>() { 1, 4, 6, 8, 9 };
    }
}