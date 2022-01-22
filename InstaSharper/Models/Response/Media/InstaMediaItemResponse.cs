using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Comment;
using InstaSharper.Models.Response.Location;
using InstaSharper.Models.Response.Product;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Media;

internal class InstaMediaItemResponse
{
    [JsonPropertyName("taken_at")]
    public long TakenAtUnixLike { get; set; }

    [JsonPropertyName("pk")]
    public long Pk { get; set; }

    [JsonPropertyName("id")]
    public string InstaIdentifier { get; set; }

    [JsonPropertyName("device_timestamp")]
    public long DeviceTimeStampUnixLike { get; set; }

    [JsonPropertyName("media_type")]
    public int MediaType { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("client_cache_key")]
    public string ClientCacheKey { get; set; }

    [JsonPropertyName("filter_type")]
    public int FilterType { get; set; }

    [JsonPropertyName("image_versions2")]
    public InstaImageCandidatesResponse Images { get; set; }

    [JsonPropertyName("video_versions")]
    public List<InstaVideoResponse> Videos { get; set; }

    [JsonPropertyName("original_width")]
    public int Width { get; set; }

    [JsonPropertyName("original_height")]
    public int Height { get; set; }

    [JsonPropertyName("user")]
    public InstaUserResponse User { get; set; }

    [JsonPropertyName("organic_tracking_token")]
    public string TrackingToken { get; set; }

    [JsonPropertyName("like_count")]
    public int LikesCount { get; set; }

    [JsonPropertyName("next_max_id")]
    public long NextMaxId { get; set; }

    [JsonPropertyName("caption")]
    public InstaCaptionResponse Caption { get; set; }

    [JsonPropertyName("comment_count")]
    public long CommentsCount { get; set; }

    [JsonPropertyName("comments_disabled")]
    public bool IsCommentsDisabled { get; set; }

    [JsonPropertyName("photo_of_you")]
    public bool PhotoOfYou { get; set; }

    [JsonPropertyName("has_liked")]
    public bool HasLiked { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("view_count")]
    public long Count { get; set; }

    [JsonPropertyName("has_audio")]
    public bool HasAudio { get; set; }

    [JsonPropertyName("usertags")]
    public InstaUserTagListResponse UserTagList { get; set; }

    [JsonPropertyName("likers")]
    public List<InstaUserShortResponse> Likers { get; set; }

    [JsonPropertyName("carousel_media")]
    public InstaCarouselResponse CarouselMedia { get; set; }

    [JsonPropertyName("location")]
    public InstaLocationResponse Location { get; set; }

    [JsonPropertyName("preview_comments")]
    public List<InstaCommentResponse> PreviewComments { get; set; }

    [JsonPropertyName("comment_likes_enabled")]
    public bool CommentLikesEnabled { get; set; }

    [JsonPropertyName("comment_threading_enabled")]
    public bool CommentThreadingEnabled { get; set; }

    [JsonPropertyName("has_more_comments")]
    public bool HasMoreComments { get; set; }

    [JsonPropertyName("max_num_visible_preview_comments")]
    public int MaxNumVisiblePreviewComments { get; set; }

    [JsonPropertyName("can_view_more_preview_comments")]
    public bool CanViewMorePreviewComments { get; set; }

    [JsonPropertyName("can_viewer_reshare")]
    public bool CanViewerReshare { get; set; }

    [JsonPropertyName("caption_is_edited")]
    public bool CaptionIsEdited { get; set; }

    [JsonPropertyName("can_viewer_save")]
    public bool CanViewerSave { get; set; }

    [JsonPropertyName("has_viewer_saved")]
    public bool HasViewerSaved { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("product_type")]
    public string ProductType { get; set; }

    [JsonPropertyName("nearly_complete_copyright_match")]
    public bool? NearlyCompleteCopyrightMatch { get; set; }

    [JsonPropertyName("number_of_qualities")]
    public int? NumberOfQualities { get; set; }

    [JsonPropertyName("video_duration")]
    public double? VideoDuration { get; set; }

    [JsonPropertyName("product_tags")]
    public InstaProductTagsContainerResponse ProductTags { get; set; }

    [JsonPropertyName("direct_reply_to_author_enabled")]
    public bool? DirectReplyToAuthorEnabled { get; set; }
}