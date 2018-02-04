using System.Collections.Generic;
using InstaSharper.Classes.Models;

namespace InstaSharper.Classes.Comparer
{
    internal class UserEqualityComparer : IEqualityComparer<InstaUserShort>
    {
        public bool Equals(InstaUserShort user, InstaUserShort anotherUser)
        {
            return user?.Pk == anotherUser?.Pk;
        }

        public int GetHashCode(InstaUserShort user)
        {
            return user.Pk.GetHashCode();
        }
    }
}