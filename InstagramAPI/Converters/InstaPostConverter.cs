using System;
using InstagramApi.Classes;
using InstagramApi.ResponseWrappers;

namespace InstagramApi.Converters
{
    internal class InstaPostConverter : IObjectConverter<InstaPost, InstaResponseItem>
    {
        public InstaResponseItem SourceObject { get; set; }

        public InstaPost Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("Source object");
            var post = new InstaPost
            {
                Link = SourceObject.Link,
                CanViewComment = SourceObject.CanViewComment,
                Code = SourceObject.Code,
                CreatedTime = SourceObject.CreatedTimeConverted,
                Localtion = SourceObject.Location,
                Images = new Images(),
                Likes = new Likes()
            };
            post.Images.LowResolution = new Image(SourceObject.Images.LowResolution.Url,
                SourceObject.Images.LowResolution.Width, SourceObject.Images.LowResolution.Height);
            post.Images.Thumbnail = new Image(SourceObject.Images.Thumbnail.Url, SourceObject.Images.Thumbnail.Width,
                SourceObject.Images.Thumbnail.Height);
            post.Images.StandartResolution = new Image(SourceObject.Images.StandartResolution.Url,
                SourceObject.Images.StandartResolution.Width, SourceObject.Images.StandartResolution.Height);
            post.Likes.Count = SourceObject.LikesCount;
            var userConverter = ConvertersFabric.GetUserConverter(SourceObject.User);
            post.User = userConverter.Convert();
            post.Likes.VisibleLikedUsers = new InstaUserList();
            foreach (var userLike in SourceObject.Likes.Users)
            {
                var userLikeConverter = ConvertersFabric.GetUserConverter(userLike);
                post.Likes.VisibleLikedUsers.Add(userLikeConverter.Convert());
            }
            return post;
        }
    }
}