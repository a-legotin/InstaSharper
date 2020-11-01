using InstaSharper.Abstractions.API.Services;

namespace InstaSharper.Abstractions.API
{
    public interface IInstaApi
    {
        public IDeviceService DeviceService { get; }
        public IUserService User { get; }
    }
}