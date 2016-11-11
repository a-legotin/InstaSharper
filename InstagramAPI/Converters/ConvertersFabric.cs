using InstagramAPI.Classes;
using InstagramAPI.ResponseWrappers;

namespace InstagramAPI.Converters
{
    internal class ConvertersFabric
    {
        internal static IObjectConverter<InstaUser, InstaUserResponse> GetUserConverter(InstaUserResponse instaresponse)
        {
            return new InstaUsersConverter { SourceObject = instaresponse };
        }

        public static IObjectConverter<InstaPost, InstaMediaItemResponse> GetSinglePostConverter(
            InstaMediaItemResponse instaresponse)
        {
            return new InstaPostConverter { SourceObject = instaresponse };
        }

        public static IObjectConverter<InstaMedia, InstaMediaItemResponse> GetSingleMediaConverter(
            InstaMediaItemResponse responseMedia)
        {
            return new InstaMediaConverter { SourceObject = responseMedia };
        }

        internal static IObjectConverter<InstaFeed, InstaFeedResponse> GetFeedConverter(
            InstaFeedResponse feedResponse)
        {
            return new InstaFeedConverter { SourceObject = feedResponse };
        }

        public static IObjectConverter<InstaMediaList, InstaMediaListResponse> GetMediaListConverter(InstaMediaListResponse mediaResponse)
        {
            return new InstaMediaListConverter { SourceObject = mediaResponse };
        }
    }
}