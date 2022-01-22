using InstaSharper.Abstractions.API;
using InstaSharper.Abstractions.API.Services;

namespace InstaSharper.API;

internal class InstaApi : IInstaApi
{
    public InstaApi(IDeviceService deviceService,
                    IUserService userService,
                    IFollowersService followers,
                    IFeedService feed,
                    IMediaService media)
    {
        Device = deviceService;
        User = userService;
        Followers = followers;
        Feed = feed;
        Media = media;
    }

    public IDeviceService Device { get; }
    public IUserService User { get; }
    public IFollowersService Followers { get; }
    public IFeedService Feed { get; }
    public IMediaService Media { get; }
}