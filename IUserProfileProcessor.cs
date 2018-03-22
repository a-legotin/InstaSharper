using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;

namespace InstaSharper.API.Processors
{
    public interface IUserProfileProcessor
    {
        Task<IResult<InstaUserShort>> SetAccountPrivateAsync();

        Task<IResult<InstaUserShort>> SetAccountPublicAsync();

        Task<IResult<bool>> ChangePasswordAsync(string oldPassword, string newPassword);

        Task<IResult<bool>> EditProfileAsync(int gender, string biography, string first_name,
            string external_url, string email, string phone, string username);
    }
}