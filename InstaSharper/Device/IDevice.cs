using System;

namespace InstaSharper.Device
{
    public interface IDevice
    {
        public Guid DeviceId { get; }
        public string AndroidId { get; }
        public string UserAgent { get; }
    }
}