using NUnit.Framework;
using System.Collections.Generic;

namespace CommonNet.Serialization.Tests
{
    public class SerializerTests
    {
        [Test]
        public void RoundTripObjectTest()
        {
            TestObject test = new TestObject()
            {
                StringValue = "Test",
                IntValue = 42,
                ArrayValue = new[] { 'a', 'b', 'c', 'z' },
                ObjectValue = new List<long> { 42, 24, 101 }
            };

            string serializedObject = Serializer.Serialize(test);
            TestObject result = Serializer.Deserialize<TestObject>(serializedObject);

            Assert.AreEqual(test.StringValue, result.StringValue);
            Assert.AreEqual(test.IntValue, result.IntValue);
            Assert.AreEqual(test.ArrayValue[0], result.ArrayValue[0]);
            Assert.AreEqual(test.ArrayValue[1], result.ArrayValue[1]);
            Assert.AreEqual(test.ArrayValue[2], result.ArrayValue[2]);
            Assert.AreEqual(test.ArrayValue[3], result.ArrayValue[3]);
            Assert.AreEqual(test.ObjectValue[0], result.ObjectValue[0]);
            Assert.AreEqual(test.ObjectValue[1], result.ObjectValue[1]);
            Assert.AreEqual(test.ObjectValue[2], result.ObjectValue[2]);

        }

        private class TestObject
        {
            public string StringValue { get; set; }
            public int IntValue { get; set; }
            public char[] ArrayValue { get; set; }
            public List<long> ObjectValue { get; set; }
        }
    }
}