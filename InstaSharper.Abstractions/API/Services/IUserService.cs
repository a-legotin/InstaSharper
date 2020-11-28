using System.Threading.Tasks;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Models.User;
using LanguageExt;

namespace InstaSharper.Abstractions.API.Services
{
    public interface IUserService
    {
        Task<Either<ResponseStatusBase, InstaUserShort>> LoginAsync();
        Task<Either<ResponseStatusBase, bool>> LogoutAsync();
    }
}