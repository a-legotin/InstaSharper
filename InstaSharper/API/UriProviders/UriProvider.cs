using InstaSharper.Abstractions.API.UriProviders;

namespace InstaSharper.API.UriProviders
{
    internal class UriProvider : IUriProvider
    {
        public UriProvider(IDeviceUriProvider deviceUriProvider, IUserUriProvider userUriProvider,
            IUserFollowersUriProvider userFollowersUriProvider)
        {
            Device = deviceUriProvider;
            User = userUriProvider;
            Followers = userFollowersUriProvider;
        }

        public IDeviceUriProvider Device { get; }
        public IUserUriProvider User { get; }
        public IUserFollowersUriProvider Followers { get; }
    }
}