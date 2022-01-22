using System.Threading.Tasks;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Feed;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.Status;
using LanguageExt;

namespace InstaSharper.Abstractions.API.Services;

public interface IFeedService
{
    Task<Either<ResponseStatusBase, InstaMediaList>> GetUserFeedAsync(long userPk,
                                                                      PaginationParameters paginationParameters);

    Task<Either<ResponseStatusBase, InstaFeed>> GetTimelineFeedAsync(PaginationParameters paginationParameters);
}