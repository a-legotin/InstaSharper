using System;

namespace InstaSharper.Abstractions.Device;

public interface IDevice
{
    Guid DeviceId { get; }
    string AndroidId { get; }
    string UserAgent { get; }
    string Jazoest { get; }
    string XigCapabilities { get; }
    string PigeonSessionId { get; set; }
    Guid PhoneId { get; set; }
}