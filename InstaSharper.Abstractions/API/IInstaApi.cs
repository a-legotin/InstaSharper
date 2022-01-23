using InstaSharper.Abstractions.API.Services;

namespace InstaSharper.Abstractions.API;

public interface IInstaApi
{
    public IUserService User { get; }
    public IFollowersService Followers { get; }
    public IFeedService Feed { get; }
    public IMediaService Media { get; }
}