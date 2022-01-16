using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.API.Services;

internal interface IApiStateProvider
{
    InstaUserShort CurrentUser { get; }
    IDevice Device { get; }
    string RankToken { get; }
    string CsrfToken { get; }
    void SetUser(InstaUserShort user);
    void PerformLogout();
}