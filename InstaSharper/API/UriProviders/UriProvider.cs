using InstaSharper.Abstractions.API.UriProviders;

namespace InstaSharper.API.UriProviders
{
    internal class UriProvider : IUriProvider
    {
        public UriProvider(IDeviceUriProvider deviceUriProvider, IUserUriProvider userUriProvider)
        {
            Device = deviceUriProvider;
            User = userUriProvider;
        }

        public IDeviceUriProvider Device { get; }
        public IUserUriProvider User { get; }
    }
}