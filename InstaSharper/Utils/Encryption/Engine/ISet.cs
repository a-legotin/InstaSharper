using System.Collections;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal interface ISet
        : ICollection
    {
        bool IsEmpty { get; }
        bool IsFixedSize { get; }
        bool IsReadOnly { get; }
        void Add(object o);
        void AddAll(IEnumerable e);
        void Clear();
        bool Contains(object o);
        void Remove(object o);
        void RemoveAll(IEnumerable e);
    }
}