using System.Threading.Tasks;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Models.User;
using LanguageExt;

namespace InstaSharper.Abstractions.API.Services;

public interface IFollowersService
{
    Task<Either<ResponseStatusBase, IInstaList<InstaUserShort>>> GetUserFollowersAsync(
        long userPk,
        PaginationParameters paginationParameters);
}