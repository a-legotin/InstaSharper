using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Infrastructure.Converters
{
    internal interface IUserConverters
    {
        IObjectConverter<InstaUserShort, InstaUserShortResponse> Self { get; }
        IObjectConverter<InstaUser, InstaUserResponse> User { get; }
    }
}