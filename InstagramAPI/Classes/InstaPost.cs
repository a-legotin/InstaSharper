using System;

namespace InstagramApi.Classes
{
    public class InstaPost
    {
        public InstaPost(InstaUser user, string code)
        {
            User = user;
            Code = code;
        }

        public InstaPost()
        {
        }

        public long UserId => User.InstaIdentifier;
        public InstaUser User { get; set; }
        public string Code { get; set; }

        public string Link { get; set; }

        public bool CanViewComment { get; set; }

        public DateTime CreatedTime { get; set; }

        public Images Images { get; set; }

        public Likes Likes { get; set; }

        public int LikesCount => Likes?.Count ?? 0;

        public static InstaPost Empty => new InstaPost(InstaUser.Empty, string.Empty);

        public InstaPostType Type { get; set; }

        public InstaLocation Localtion { get; set; }

        public virtual bool Equals(InstaPost post)
        {
            if (Code != post.Code) return false;
            if (Type != post.Type) return false;
            if (UserId != post.UserId) return false;
            return true;
        }
    }
}