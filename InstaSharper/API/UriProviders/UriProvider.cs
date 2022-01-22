using InstaSharper.Abstractions.API.UriProviders;

namespace InstaSharper.API.UriProviders;

internal class UriProvider : IUriProvider
{
    public UriProvider(IDeviceUriProvider deviceUriProvider,
                       IUserUriProvider userUriProvider,
                       IUserFollowersUriProvider userFollowersUriProvider,
                       IFeedUriProvider feed)
    {
        Device = deviceUriProvider;
        User = userUriProvider;
        Followers = userFollowersUriProvider;
        Feed = feed;
    }

    public IFeedUriProvider Feed { get; }
    public IDeviceUriProvider Device { get; }
    public IUserUriProvider User { get; }
    public IUserFollowersUriProvider Followers { get; }
}