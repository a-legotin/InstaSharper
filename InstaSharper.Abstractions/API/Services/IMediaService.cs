using System.Threading.Tasks;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.Status;
using LanguageExt;

namespace InstaSharper.Abstractions.API.Services;

public interface IMediaService
{
    Task<Either<ResponseStatusBase, InstaMediaList>> GetMediaListByIdsAsync(params string[] mediaIds);
}