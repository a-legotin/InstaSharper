using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;

namespace InstaSharper.API.Processors
{
    public interface IFeedProcessor
    {
        Task<IResult<InstaTagFeed>> GetTagFeedAsync(string tag, PaginationParameters paginationParameters);
        Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(PaginationParameters paginationParameters);
        Task<IResult<InstaExploreFeed>> GetExploreFeedAsync(PaginationParameters paginationParameters);
        Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityFeedAsync(PaginationParameters paginationParameters);
        Task<IResult<InstaActivityFeed>> GetRecentActivityFeedAsync(PaginationParameters paginationParameters);
        Task<IResult<InstaMediaList>> GetLikeFeedAsync(PaginationParameters paginationParameters);
    }
}