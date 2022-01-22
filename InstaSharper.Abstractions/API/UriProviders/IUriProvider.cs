namespace InstaSharper.Abstractions.API.UriProviders;

public interface IUriProvider
{
    IDeviceUriProvider Device { get; }
    IUserUriProvider User { get; }
    IUserFollowersUriProvider Followers { get; }
    IFeedUriProvider Feed { get; }
}