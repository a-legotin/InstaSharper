using InstaSharper.Abstractions.API;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.API.Services;

namespace InstaSharper.API
{
    internal class InstaApi : IInstaApi
    {
        public InstaApi(IDeviceService deviceService, UserService userService)
        {
            Device = deviceService;
            User = userService;
        }

        public IDeviceService Device { get; }
        public IUserService User { get; }
    }
}