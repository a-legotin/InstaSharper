using System;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.Comment;
using InstaSharper.Models.Response.User;
using InstaSharper.Utils;

namespace InstaSharper.Infrastructure.Converters.Media;

internal class CommentConverter : IObjectConverter<InstaComment, InstaCommentResponse>
{
    private readonly IObjectConverter<InstaUserShort, InstaUserShortResponse> _userPreviewConverter;

    public CommentConverter(IObjectConverter<InstaUserShort, InstaUserShortResponse> userPreviewConverter)
    {
        _userPreviewConverter = userPreviewConverter;
    }

    public InstaComment Convert(InstaCommentResponse source)
    {
        var comment = new InstaComment
        {
            BitFlags = source.BitFlags,
            ContentType = (InstaContentType)Enum.Parse(typeof(InstaContentType), source.ContentType, true),
            CreatedAt = DateTimeHelper.UnixTimestampToDateTime(source.CreatedAt),
            CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(source.CreatedAtUtc),
            LikesCount = source.LikesCount,
            Pk = source.Pk,
            Status = source.Status,
            Text = source.Text,
            Type = source.Type,
            UserId = source.UserId,
            User = _userPreviewConverter.Convert(source.User),
            DidReportAsSpam = source.DidReportAsSpam,
            ChildCommentCount = source.ChildCommentCount,
            HasLikedComment = source.HasLikedComment,
            HasMoreHeadChildComments = source.HasMoreHeadChildComments,
            HasMoreTailChildComments = source.HasMoreTailChildComments
        };
        return comment;
    }
}