using InstagramApi.Classes;
using InstagramApi.ResponseWrappers;
using InstagramAPI.ResponseWrappers;

namespace InstagramApi.Converters
{
    internal class ConvertersFabric
    {
        internal static IObjectConverter<InstaPostList, InstaResponsePostList> GetPostsConverter(InstaResponsePostList instaresponse)
        {
            return new InstaPostsConverter { SourceObject = instaresponse };
        }

        internal static IObjectConverter<InstaUser, InstaUserResponse> GetUserConverter(InstaUserResponse instaresponse)
        {
            return new InstaUsersConverter { SourceObject = instaresponse };
        }

        public static IObjectConverter<InstaPost, InstaUserFeedItemResponse> GetSinglePostConverter(
            InstaUserFeedItemResponse instaresponse)
        {
            return new InstaPostConverter { SourceObject = instaresponse };
        }

        public static IObjectConverter<InstaMedia, InstaResponseMedia> GetSingleMediaConverter(
            InstaResponseMedia responseMedia)
        {
            return new InstaMediaConverter { SourceObject = responseMedia };
        }

        internal static IObjectConverter<InstaFeed, InstaFeedResponse> GetFeedConverter(
            InstaFeedResponse feedResponse)
        {
            return new InstaFeedConverter { SourceObject = feedResponse };
        }
    }
}