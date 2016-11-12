using InstagramAPI.Classes.Models;
using InstagramAPI.ResponseWrappers;

namespace InstagramAPI.Converters
{
    internal class ConvertersFabric
    {
        internal static IObjectConverter<InstaUser, InstaUserResponse> GetUserConverter(InstaUserResponse instaresponse)
        {
            return new InstaUsersConverter { SourceObject = instaresponse };
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

        public static IObjectConverter<InstaCaption, InstaCaptionResponse> GetCaptionConverter(InstaCaptionResponse captionResponse)
        {
            return new InstaCaptionConverter { SourceObject = captionResponse };
        }

        public static IObjectConverter<InstaFriendshipStatus, InstaFriendshipStatusResponse> GetFriendShipStatusConverter(InstaFriendshipStatusResponse friendshipStatusResponse)
        {
            return new InstaFriendshipStatusConverter { SourceObject = friendshipStatusResponse };
        }
    }
}