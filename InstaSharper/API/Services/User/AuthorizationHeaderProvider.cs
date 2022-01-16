namespace InstaSharper.API.Services;

internal class AuthorizationHeaderProvider : IAuthorizationHeaderProvider
{
    public string AuthorizationHeader { get; set; }
    public string WwwClaimHeader { get; set; }
    public long CurrentUserIdHeader { get; set; } = 0;
    public string ShbId { get; set; }
    public string ShbTs { get; set; }
    public string Rur { get; set; }
}