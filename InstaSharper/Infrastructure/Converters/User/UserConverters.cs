using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Infrastructure.Converters.User
{
    internal class UserConverters : IUserConverters
    {
        public UserConverters(IObjectConverter<InstaUserShort, InstaUserShortResponse> self) => Self = self;

        public IObjectConverter<InstaUserShort, InstaUserShortResponse> Self { get; }
    }
}