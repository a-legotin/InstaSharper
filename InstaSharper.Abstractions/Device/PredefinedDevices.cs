using System;
using InstaSharper.Device;

namespace InstaSharper.Abstractions.Device
{
    internal static class PredefinedDevices
    {
        public static IDevice Xiaomi4Prime
            = new AndroidDevice(Guid.Parse("2f5645f0-a663-4c50-91b7-e0e2e3c3981e"),
                @"Instagram 165.1.0.29.119 Android (28/9; 440dpi; 1080x1920; Xiaomi; Redmi 4 Prime; markw; qcom; en_US; 253447809)");
    }
}