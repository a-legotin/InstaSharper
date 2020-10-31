using InstaSharper.Serialization;
using NUnit.Framework;

namespace InstaSharper.Tests
{
    [TestFixture]
    [Category("AlwaysRun")]
    public class SerializerTest
    {
        private class DummyObject
        {
            public string Value { get; set; }
        }

        [Test]
        public void DeSerializationTest()
        {
            var serializer = new JsonSerializer();
            var deserialized = serializer.Deserialize<DummyObject>("{\"Value\":\"testvalue\"}");

            Assert.AreEqual(deserialized.Value,  "testvalue");
        }

        [Test]
        public void SerializationTest()
        {
            var dummy = new DummyObject
            {
                Value = "testvalue"
            };

            var serializer = new JsonSerializer();
            var serialized = serializer.Serialize(dummy);

            Assert.AreEqual("{\"Value\":\"testvalue\"}", serialized);
        }
    }
}