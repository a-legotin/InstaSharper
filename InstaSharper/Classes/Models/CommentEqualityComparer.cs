using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    class CommentEqualityComparer : IEqualityComparer<InstaComment>
    {
        public bool Equals(InstaComment comment, InstaComment anotherComment)
        {
            return comment?.Pk == anotherComment?.Pk;
        }

        public int GetHashCode(InstaComment comment)
        {
            return comment.Pk.GetHashCode();
        }
    }
}