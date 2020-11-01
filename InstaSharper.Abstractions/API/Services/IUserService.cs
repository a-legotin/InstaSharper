using System.Threading.Tasks;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Status;
using LanguageExt;

namespace InstaSharper.Abstractions.API.Services
{
    public interface IUserService
    {
        Task<Either<ResponseStatusBase, LoginResponse>> LoginAsync();
    }
}