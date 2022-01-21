using System;

namespace InstaSharper.Abstractions.Device;

public interface IDevice
{
    public Guid DeviceId { get; }
    public string AndroidId { get; }
    public string UserAgent { get; }
    string Jazoest { get; }
    string XigCapabilities { get; }
    string PigeonSessionId { get; set; }
}