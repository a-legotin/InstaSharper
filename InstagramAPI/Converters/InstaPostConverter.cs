using System;
using InstagramApi.Classes;
using InstagramApi.ResponseWrappers;

namespace InstagramApi.Converters
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