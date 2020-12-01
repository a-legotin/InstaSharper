using InstaSharper.Abstractions.API.Services;

namespace InstaSharper.Abstractions.API
{
    public interface IInstaApi
    {
        public IDeviceService Device { get; }
        public IUserService User { get; }
    }
}