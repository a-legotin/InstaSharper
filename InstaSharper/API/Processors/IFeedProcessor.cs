using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;

namespace InstaSharper.API.Processors
{
    public interface IFeedProcessor
    {
        Task<IResult<InstaTagFeed>> GetTagFeedAsync(string tag, int maxPages = 0);
        Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(int maxPages = 0);
        Task<IResult<InstaExploreFeed>> GetExploreFeedAsync(int maxPages = 0);
        Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityFeedAsync(int maxPages = 0);
        Task<IResult<InstaActivityFeed>> GetRecentActivityFeedAsync(int maxPages = 0);
        Task<IResult<InstaMediaList>> GetLikeFeedAsync(int maxPages = 0);
    }
}