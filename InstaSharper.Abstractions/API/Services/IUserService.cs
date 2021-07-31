using System.Threading.Tasks;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Models.User;
using LanguageExt;

namespace InstaSharper.Abstractions.API.Services
{
    public interface IUserService
    {
        bool IsAuthenticated { get; }
        Task<Either<ResponseStatusBase, InstaUserShort>> LoginAsync();
        Task<Either<ResponseStatusBase, bool>> LogoutAsync();
        Task<Either<ResponseStatusBase, InstaUser>> GetUserAsync(string username);
        Task<Either<ResponseStatusBase, InstaUser[]>> SearchUsersAsync(string query);
        byte[] GetUserSessionAsByteArray();
        void LoadStateDataFromBytes(byte[] stateBytes);
    }
}