namespace InstaSharper.Abstractions.API.UriProviders
{
    public interface IUriProvider
    {
        public IDeviceUriProvider Device { get; }

        public IUserUriProvider User { get; }
    }
}