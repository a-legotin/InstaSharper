using System;
using InstagramAPI.Classes;
using InstagramAPI.Helpers;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaResponseItem
    {
        public string Code { get; set; }
        public InstaLocation Location { get; set; }

        public string Link { get; set; }

        public InstaPostType Type { get; set; }

        public string Id { get; set; }

        public ImagesResponse Images { get; set; }

        public LikesResponse Likes { get; set; }

        public int LikesCount => Likes?.Count ?? 0;

        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("can_view_comments")]
        public bool CanViewComment { get; set; }

        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }

        public DateTime CreatedTimeConverted => DateTimeHelper.UnixTimestampToDateTime(double.Parse(CreatedTime));
    }
}