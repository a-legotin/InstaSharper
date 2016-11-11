using System.Threading.Tasks;
using InstagramAPI.Classes;

namespace InstagramAPI.API
{
    public interface IInstaApi
    {
        bool IsUserAuthenticated { get; }
        IResult<InstaUser> GetUser(string username);
        Task<IResult<InstaUser>> GetUserAsync(string username);
        IResult<InstaPostList>  GetUserPosts(string username);
        Task<IResult<InstaPostList>> GetUserPostsAsync(string username);
        IResult<InstaMedia>  GetMediaByCode(string postCode);
        Task<IResult<InstaMedia>> GetMediaByCodeAsync(string postCode);
        IResult<bool>  Login();
        Task<IResult<bool>> LoginAsync();
        IResult<InstaFeed>  GetUserFeed(int pageCount);
        Task<IResult<InstaFeed>> GetUserFeedAsync(int pageCount);
    }
}