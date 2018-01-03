using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;

namespace InstaSharper.API.Processors
{
    public interface ICommentProcessor
    {
        Task<IResult<InstaCommentList>>
            GetMediaCommentsAsync(string mediaId, PaginationParameters paginationParameters);

        Task<IResult<InstaComment>> CommentMediaAsync(string mediaId, string text);
        Task<IResult<bool>> DeleteCommentAsync(string mediaId, string commentId);
    }
}