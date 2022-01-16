using InstaSharper.Abstractions.API.Services;
using InstaSharper.API.Services.Followers;

namespace InstaSharper.Abstractions.API;

public interface IInstaApi
{
    public IDeviceService Device { get; }
    public IUserService User { get; }
    public IFollowersService Followers { get; }
}