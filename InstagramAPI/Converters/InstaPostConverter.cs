using System;
using InstagramAPI.Classes;
using InstagramAPI.ResponseWrappers;

namespace InstagramAPI.Converters
{
    internal class InstaPostConverter : IObjectConverter<InstaPost, InstaUserFeedItemResponse>
    {
        public InstaUserFeedItemResponse SourceObject { get; set; }

        public InstaPost Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("Source object");
            var post = new InstaPost
            {
                Code = SourceObject.Code,
                Images = new Images(),
                Likes = new Likes()
            };

            return post;
        }
    }
}