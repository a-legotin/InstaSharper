using InstaSharper.Abstractions.API;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.API.Services;

namespace InstaSharper.API
{
    internal class InstaApi : IInstaApi
    {
        public InstaApi(IDeviceService deviceService, UserService userService)
        {
            DeviceService = deviceService;
            User = userService;
        }

        public IDeviceService DeviceService { get; }
        public IUserService User { get; }
    }
}