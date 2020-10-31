using NUnit.Framework;

namespace InstaSharper.Tests
{
    [TestFixture]
    [Category("AlwaysRun")]
    public class DummyTest
    {
        [Test]
        public void IsTrueReturned()
        {
            var isTrue = true;
            Assert.IsTrue(isTrue, "should always return true");
        }
    }
}