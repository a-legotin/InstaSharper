namespace InstaSharper.API.Services;

internal interface IAuthorizationHeaderProvider
{
    string AuthorizationHeader { get; set; }
    string WwwClaimHeader { get; set; }
    long CurrentUserIdHeader { get; set; }
    string ShbId { get; set; }
    string ShbTs { get; set; }
    string Rur { get; set; }
    string FbTripHeader { get; set; }
    string XMidHeader { get; set; }
}