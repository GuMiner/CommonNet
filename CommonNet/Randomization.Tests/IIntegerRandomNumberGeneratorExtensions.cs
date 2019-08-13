using CommonNet.Randomization;
using Moq;
using NUnit.Framework;
using System;

namespace Tests
{
    [Parallelizable]
    public class IIntegerRandomNumberGeneratorExtensions
    {
        [Test]
        public void NextIntTestsArguments()
        {
            Mock<IIntegerRandomNumberGenerator> generator = new Mock<IIntegerRandomNumberGenerator>(MockBehavior.Strict);

            Assert.Throws<ArgumentOutOfRangeException>(() => generator.Object.NextInt(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => generator.Object.NextInt(-1));
        }

        [Test]
        public void NextIntTests()
        {
            Mock<IIntegerRandomNumberGenerator> generator = new Mock<IIntegerRandomNumberGenerator>(MockBehavior.Strict);
            generator.Setup(gen => gen.NextInt()).Returns(0);

            Assert.AreEqual(0, generator.Object.NextInt(1));

            // Technically these are internal implementation details, but good to validate anywyas.
            generator.Setup(gen => gen.NextInt()).Returns(4);
            Assert.AreEqual(4, generator.Object.NextInt(10));
            Assert.AreEqual(0, generator.Object.NextInt(4));
        }
    }
}