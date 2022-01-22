using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Comment;
using InstaSharper.Models.Response.Media;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryItemResponse
{
    [JsonPropertyName("show_one_tap_fb_share_tooltip")]
    public bool ShowOneTapTooltip { get; set; }

    [JsonPropertyName("has_liked")]
    public bool HasLiked { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("caption")]
    public InstaCaptionResponse Caption { get; set; }

    [JsonPropertyName("can_reshare")]
    public bool CanReshare { get; set; }

    [JsonPropertyName("ad_action")]
    public string AdAction { get; set; }

    [JsonPropertyName("can_viewer_save")]
    public bool CanViewerSave { get; set; }

    [JsonPropertyName("caption_position")]
    public double CaptionPosition { get; set; }

    [JsonPropertyName("caption_is_edited")]
    public bool CaptionIsEdited { get; set; }

    [JsonPropertyName("client_cache_key")]
    public string ClientCacheKey { get; set; }

    [JsonPropertyName("device_timestamp")]
    public long DeviceTimestamp { get; set; }

    [JsonPropertyName("comment_likes_enabled")]
    public bool CommentLikesEnabled { get; set; }

    [JsonPropertyName("comment_count")]
    public long CommentCount { get; set; }

    [JsonPropertyName("comment_threading_enabled")]
    public bool CommentThreadingEnabled { get; set; }

    [JsonPropertyName("filter_type")]
    public long FilterType { get; set; }

    [JsonPropertyName("expiring_at")]
    public long ExpiringAt { get; set; }

    [JsonPropertyName("has_audio")]
    public bool HasAudio { get; set; }

    [JsonPropertyName("link_text")]
    public string LinkText { get; set; }

    [JsonPropertyName("pk")]
    public long Pk { get; set; }

    [JsonPropertyName("is_dash_eligible")]
    public long? IsDashEligible { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("has_more_comments")]
    public bool HasMoreComments { get; set; }

    [JsonPropertyName("image_versions2")]
    public InstaImageCandidatesResponse Images { get; set; }

    [JsonPropertyName("like_count")]
    public long LikeCount { get; set; }

    [JsonPropertyName("is_reel_media")]
    public bool IsReelMedia { get; set; }

    [JsonPropertyName("likers")]
    public List<InstaUserShortResponse> Likers { get; set; }

    [JsonPropertyName("organic_tracking_token")]
    public string OrganicTrackingToken { get; set; }

    [JsonPropertyName("media_type")]
    public long MediaType { get; set; }

    [JsonPropertyName("max_num_visible_preview_comments")]
    public long MaxNumVisiblePreviewComments { get; set; }

    [JsonPropertyName("number_of_qualities")]
    public long NumberOfQualities { get; set; }

    [JsonPropertyName("original_width")]
    public long OriginalWidth { get; set; }

    [JsonPropertyName("original_height")]
    public long OriginalHeight { get; set; }

    [JsonPropertyName("photo_of_you")]
    public bool PhotoOfYou { get; set; }

    [JsonPropertyName("story_sticker_ids")]
    public string StoryStickerIds { get; set; }

    [JsonPropertyName("timezone_offset")]
    public double TimezoneOffset { get; set; }

    [JsonPropertyName("story_is_saved_to_archive")]
    public bool StoryIsSavedToArchive { get; set; }

    [JsonPropertyName("viewer_count")]
    public double ViewerCount { get; set; }

    [JsonPropertyName("total_viewer_count")]
    public double TotalViewerCount { get; set; }

    [JsonPropertyName("viewer_cursor")]
    public string ViewerCursor { get; set; }

    [JsonPropertyName("has_shared_to_fb")]
    public double HasSharedToFb { get; set; }

    [JsonPropertyName("story_events")]
    public List<object> StoryEvents { get; set; }

    [JsonPropertyName("story_polls")]
    public List<InstaStoryPollItemResponse> StoryPolls { get; set; }

    [JsonPropertyName("story_sliders")]
    public List<InstaStorySliderItemResponse> StorySliders { get; set; }

    [JsonPropertyName("story_questions")]
    public List<InstaStoryQuestionItemResponse> StoryQuestions { get; set; }

    [JsonPropertyName("story_question_responder_infos")]
    public List<InstaStoryQuestionInfoResponse> StoryQuestionsResponderInfos { get; set; }

    [JsonPropertyName("reel_mentions")]
    public List<InstaReelMentionResponse> ReelMentions { get; set; }

    [JsonPropertyName("preview_comments")]
    public List<InstaCommentResponse> PreviewComments { get; set; }

    [JsonPropertyName("story_hashtags")]
    public List<InstaReelMentionResponse> StoryHashtags { get; set; }

    [JsonPropertyName("story_feed_media")]
    public List<InstaStoryFeedMediaResponse> StoryFeedMedia { get; set; }

    [JsonPropertyName("story_locations")]
    public List<InstaStoryLocationResponse> StoryLocations { get; set; }

    [JsonPropertyName("taken_at")]
    public long TakenAt { get; set; }

    [JsonPropertyName("imported_taken_at")]
    public long ImportedTakenAt { get; set; }

    [JsonPropertyName("video_dash_manifest")]
    public string VideoDashManifest { get; set; }

    [JsonPropertyName("supports_reel_reactions")]
    public bool SupportsReelReactions { get; set; }

    [JsonPropertyName("user")]
    public InstaUserPkResponse User { get; set; }

    [JsonPropertyName("video_duration")]
    public double VideoDuration { get; set; }

    [JsonPropertyName("video_versions")]
    public List<InstaVideoResponse> VideoVersions { get; set; }

    [JsonPropertyName("story_cta")]
    public List<InstaStoryCTAContainerResponse> StoryCTA { get; set; }

    [JsonPropertyName("story_poll_voter_infos")]
    public List<InstaStoryPollVoterInfoItemResponse> StoryPollVoters { get; set; }

    [JsonPropertyName("story_slider_voter_infos")]
    public List<InstaStorySliderVoterInfoItemResponse> StorySliderVoters { get; set; }

    [JsonPropertyName("viewers")]
    public List<InstaUserShortResponse> Viewers { get; set; }

    [JsonPropertyName("story_countdowns")]
    public List<InstaStoryCountdownItemResponse> Countdowns { get; set; }
}