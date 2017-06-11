using System.Collections.Generic;
using InstaSharper.Classes.Models;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaStoryItemResponse
    {
        [JsonProperty("taken_at")]
        public long TakenAt { get; set; }

        [JsonProperty("pk")]
        public long Pk { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("device_timestamp")]
        public long DeviceTimeStamp { get; set; }

        [JsonProperty("media_type")]
        public InstaMediaType MediaType { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("client_cache_key")]
        public string ClientCacheKey { get; set; }

        [JsonProperty("filter_type")]
        public int FilterType { get; set; }

        [JsonProperty("image_versions2")]
        public InstaImageCandidatesResponse ImageVersions { get; set; }

        [JsonProperty("original_width")]
        public int OriginalWidth { get; set; }

        [JsonProperty("original_height")]
        public int OriginalHeight { get; set; }

        [JsonProperty("caption_position")]
        public double CaptionPosition { get; set; }

        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }

        [JsonProperty("organic_tracking_token")]
        public string TrackingToken { get; set; }

        [JsonProperty("like_count")]
        public int LikeCount { get; set; }

        [JsonProperty("likers")]
        public List<InstaUserResponse> Likers { get; set; }

        [JsonProperty("usertags")]
        public InstaUserTagListResponse UserTags { get; set; }

        [JsonProperty("carousel_media")]
        public InstaCarouselResponse CarouselMedia { get; set; }

        [JsonProperty("has_liked")]
        public bool HasLiked { get; set; }

        [JsonProperty("has_more_comments")]
        public bool HasMoreComments { get; set; }

        [JsonProperty("max_num_visible_preview_comments")]
        public int MaxNumVisiblePreviewComments { get; set; }

        //public InstaComment PreviewComments { get; set; }  --- ---  //I'll check what is.

        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        [JsonProperty("comment_disabled")]
        public bool CommentsDisabled { get; set; }

        [JsonProperty("caption")]
        public InstaCaptionResponse Caption { get; set; }

        [JsonProperty("caption_is_edited")]
        public bool CaptionIsEdited { get; set; } //Visible only if the story is an image.

        [JsonProperty("photo_of_you")]
        public bool PhotoOfYou { get; set; }

        [JsonProperty("can_viewer_save")]
        public bool CanViewerSave { get; set; }

        [JsonProperty("expiring_at")]
        public long ExpiringAt { get; set; }

        [JsonProperty("is_reel_media")]
        public bool IsReelMedia { get; set; }

        //public List<InstaReel> ReelMentions { get; set; }  --- ---  //I'll do a test via Fiddler

        //[JsonProperty("story_locations")]

        //public List<InstaLocation> StoryLocation { get; set; }

        //public List<string> StoryHashtags { get; set; } //I'll do a test via Fiddler

        #region Video

        [JsonProperty("video_versions")]
        public InstaVideoCandidatesResponse VideoVersions { get; set; } //Visible only if the story is a video.

        [JsonProperty("has_audio")]
        public bool HasAudio { get; set; } //Visible only if the story is a video.

        [JsonProperty("video_duration")]
        public double VideoDuration { get; set; } //Visible only if the story is a video.

        #endregion
    }
}