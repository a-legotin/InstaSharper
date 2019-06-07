﻿using System;

namespace InstaSharper.Classes
{
    public class RequestDelay : IRequestDelay
    {
        private readonly int _maxSeconds;
        private readonly int _minSeconds;

        private readonly Random _random;
        private bool _isEnabled;

        private RequestDelay(int minSeconds, int maxSeconds)
        {
            _minSeconds = minSeconds;
            _maxSeconds = maxSeconds;
            _random = new Random(DateTime.Now.Millisecond);
            _isEnabled = true;
        }

        public TimeSpan Value => Exist ? TimeSpan.FromSeconds(_random.Next(_minSeconds, _maxSeconds)) : TimeSpan.Zero;

        public bool Exist => _isEnabled && _minSeconds != 0 && _maxSeconds != 0;

        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        public static IRequestDelay FromSeconds(int min, int max)
        {
            if (min > max) throw new ArgumentException("Value max should be bigger that value min");

            if (max < 0) throw new ArgumentException("Both min and max values should be bigger than 0");

            return new RequestDelay(min, max);
        }

        public static IRequestDelay Empty()
        {
            return new RequestDelay(0, 0);
        }
    }
}