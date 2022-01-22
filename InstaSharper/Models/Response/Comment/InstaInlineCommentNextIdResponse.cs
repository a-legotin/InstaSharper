using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Comment;

internal class InstaInlineCommentNextIdResponse
{
    [JsonPropertyName("cached_comments_cursor")]
    public string CachedCommentsCursor { get; set; }

    [JsonPropertyName("bifilter_token")]
    public string BifilterToken { get; set; }

    [JsonPropertyName("server_cursor")]
    public string ServerCursor { get; set; }
}