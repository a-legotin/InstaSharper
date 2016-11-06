using System.Threading.Tasks;
using InstagramApi.Classes;
using InstagramApi.Classes.Web;

namespace InstagramApi.API
{
    public interface IInstaApi
    {
        bool IsUserAuthenticated { get; }
        InstaUser GetUser();
        Task<InstaUser> GetUserAsync();
        InstaPostList GetUserPosts();
        Task<InstaPostList> GetUserPostsAsync();
        InstaMedia GetMediaByCode(string postCode);
        Task<InstaMedia> GetMediaByCodeAsync(string postCode);
        bool Login();
        Task<bool> LoginAsync();
        InstaUserFeed GetUserFeed(int pageCount);
        Task<InstaUserFeed> GetUserFeedAsync(int pageCount);
    }
}