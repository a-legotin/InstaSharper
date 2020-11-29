using System;
using InstaSharper.Tests.Integration;
using NUnit.Framework;

namespace InstaSharper.Tests.Unit
{
    public class BuilderTest : UnitTestBase
    {
        [Test]
        public void BuilderThrowsOnCredentials()
        {
            Assert.Throws<ArgumentException>(() => Builder.Builder.Create().Build(),
                "builder must throw on empty credentials");
            Assert.Throws<ArgumentException>(() => Builder.Builder.Create().WithUserCredentials("", "pass").Build(),
                "builder must throw on empty username");
            Assert.Throws<ArgumentException>(() => Builder.Builder.Create().WithUserCredentials("user", "").Build(),
                "builder must throw on empty password");
            Assert.DoesNotThrow(() => Builder.Builder.Create().WithUserCredentials("user", "pass").Build(),
                "builder must not throw on valid credentials");
        }
    }
}