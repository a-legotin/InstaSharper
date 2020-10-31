using InstaSharper.Abstractions.API;
using InstaSharper.Abstractions.API.Actions;
using InstaSharper.Abstractions.Device;

namespace InstaSharper.API
{
    internal class InstaApi : IInstaApi
    {
        public InstaApi(IDevice device)
        {
        }

        public IUserActions User { get; }
    }
}