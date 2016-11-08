using System.Threading.Tasks;
using InstagramApi.Classes;

namespace InstagramApi.API
{
    public interface IInstaApi
    {
        bool IsUserAuthenticated { get; }
        InstaUser GetUser(string username);
        Task<InstaUser> GetUserAsync(string username);
        InstaPostList GetUserPosts();
        Task<InstaPostList> GetUserPostsAsync();
        InstaMedia GetMediaByCode(string postCode);
        Task<InstaMedia> GetMediaByCodeAsync(string postCode);
        bool Login();
        Task<bool> LoginAsync();
        InstaFeed GetUserFeed(int pageCount);
        Task<InstaFeed> GetUserFeedAsync(int pageCount);
    }
}