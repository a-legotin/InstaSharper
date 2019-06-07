using System;
using InstaSharper.Classes;
using Xunit;

namespace InstaSharper.Tests.Classes
{
    [Trait("Category", "Classes")]
    public class RequestDelayTest
    {
        [Fact]
        public void DelayDisableTest()
        {
            var delay = RequestDelay.FromSeconds(1, 2);
            delay.Disable();
            Assert.False(delay.Exist);
        }

        [Fact]
        public void DelayDisableValueTest()
        {
            var s = 5;
            var delay = RequestDelay.FromSeconds(s, s);
            delay.Disable();
            Assert.Equal(TimeSpan.Zero, delay.Value);
        }

        [Fact]
        public void DelayEmptyNotExistTest()
        {
            var delay = RequestDelay.Empty();
            Assert.False(delay.Exist);
        }

        [Fact]
        public void DelayEmptyValueTest()
        {
            var delay = RequestDelay.Empty();
            Assert.True(delay.Value.Seconds == 0);
        }

        [Fact]
        public void DelayEnableTest()
        {
            var delay = RequestDelay.FromSeconds(1, 2);
            delay.Disable();
            delay.Enable();
            Assert.True(delay.Exist);
        }

        [Fact]
        public void DelayExistTest()
        {
            var delay = RequestDelay.FromSeconds(1, 2);
            Assert.True(delay.Exist);
        }

        [Fact]
        public void DelayFactoryExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => RequestDelay.FromSeconds(2, 1));
        }

        [Fact]
        public void DelayFactoryNegativeValuesExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => RequestDelay.FromSeconds(-2, -1));
        }

        [Fact]
        public void DelayNotExistTest()
        {
            var delay = RequestDelay.FromSeconds(0, 0);
            Assert.False(delay.Exist);
        }

        [Fact]
        public void DelaySameValueTest()
        {
            var s = 5;
            var delay = RequestDelay.FromSeconds(s, s);
            Assert.True(delay.Value.Seconds == s);
        }
    }
}