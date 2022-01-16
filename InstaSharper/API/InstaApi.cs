using InstaSharper.Abstractions.API;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.API.Services.Followers;

namespace InstaSharper.API;

internal class InstaApi : IInstaApi
{
    public InstaApi(IDeviceService deviceService, IUserService userService, IFollowersService followers)
    {
        Device = deviceService;
        User = userService;
        Followers = followers;
    }

    public IDeviceService Device { get; }
    public IUserService User { get; }
    public IFollowersService Followers { get; }
}