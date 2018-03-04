using System;

namespace InstaSharper.Classes
{
    public class RequestDelay
    {
        private RequestDelay(int minSeconds, int maxSeconds)
        {
            _minSeconds = minSeconds;
            _maxSeconds = maxSeconds;
            _random = new Random(DateTime.Now.Millisecond);
        }

        public static RequestDelay FromSeconds(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException("Value max should be bigger that value min");
            }

            if (max < 0)
            {
                throw new ArgumentException("Both min and max values should be bigger than 0");
            }

            return new RequestDelay(min, max);
        }

        public static RequestDelay Empty()
        {
            return new RequestDelay(0, 0);
        }

        private readonly Random _random;
        private readonly int _minSeconds;
        private readonly int _maxSeconds;

        public TimeSpan Value => TimeSpan.FromSeconds(_random.Next(_minSeconds, _maxSeconds));

        public bool Exist => _minSeconds != 0 && _maxSeconds != 0;
    }
}