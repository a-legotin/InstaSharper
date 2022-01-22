using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Infrastructure.Converters.User;

internal class UserConverters : IUserConverters
{
    public UserConverters(IObjectConverter<InstaUserShort, InstaUserShortResponse> self,
                          IObjectConverter<InstaUser, InstaUserResponse> userConverter)
    {
        Self = self;
        User = userConverter;
    }

    public IObjectConverter<InstaUserShort, InstaUserShortResponse> Self { get; }
    public IObjectConverter<InstaUser, InstaUserResponse> User { get; }
}