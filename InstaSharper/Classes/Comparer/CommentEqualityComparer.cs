using System.Collections.Generic;
using InstaSharper.Classes.Models;

namespace InstaSharper.Classes.Comparer
{
    internal class CommentEqualityComparer : IEqualityComparer<InstaComment>
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