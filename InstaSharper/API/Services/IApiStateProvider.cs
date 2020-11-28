using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.API.Services
{
    internal interface IApiStateProvider
    {
        IDevice Device { get; }
        public string RankToken { get; }
        public string CsrfToken { get; }
        void SetUser(InstaUserShort user);
        void PerformLogout();
    }
}