using System;
using InstaSharper.Abstractions.Device;

namespace InstaSharper.Models.Device;

internal static class PredefinedDevices
{
    public static IDevice Xiaomi4Prime
        = new AndroidDevice(Guid.Parse("90182feb-5663-4ebd-b996-2f94eec42235"),
            @"Instagram 207.0.0.39.120 Android (28/9; 440dpi; 1080x1920; Xiaomi; Redmi 4 Prime; markw; qcom; en_US; 321039115)");
}