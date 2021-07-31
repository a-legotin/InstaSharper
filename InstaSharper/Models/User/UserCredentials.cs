using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Models.User
{
    internal class UserCredentials : IUserCredentials
    {
        public UserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }

    internal class InstaUserShortList : IInstaList<InstaUserShort>
    {
        private readonly List<InstaUserShort> _innerList = new List<InstaUserShort>();

        public InstaUserShortList()
        {
        }

        public InstaUserShortList(IEnumerable<InstaUserShort> users)
        {
            _innerList.AddRange(users);
        }

        public string NextMaxId { get; set; }
        public IEnumerator<InstaUserShort> GetEnumerator() => _innerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(InstaUserShort item) => _innerList.Add(item);

        public void Clear() => _innerList.Clear();

        public bool Contains(InstaUserShort item) => _innerList.Any(user => user.Pk == item?.Pk);

        public void CopyTo(InstaUserShort[] array, int arrayIndex) => _innerList.CopyTo(array, arrayIndex);

        public bool Remove(InstaUserShort item) => _innerList.Remove(item);

        public int Count => _innerList.Count;

        public bool IsReadOnly => false;
    }
}