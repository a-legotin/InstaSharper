using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    class UserEqualityComparer : IEqualityComparer<InstaUserShort>
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