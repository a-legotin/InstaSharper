using System;
using System.Collections;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal abstract class UnmodifiableDictionary
        : IDictionary
    {
        public virtual void Add(object k, object v)
        {
            throw new NotSupportedException();
        }

        public virtual void Clear()
        {
            throw new NotSupportedException();
        }

        public abstract bool Contains(object k);

        public abstract void CopyTo(Array array, int index);

        public abstract int Count { get; }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public abstract IDictionaryEnumerator GetEnumerator();

        public virtual void Remove(object k)
        {
            throw new NotSupportedException();
        }

        public abstract bool IsFixedSize { get; }

        public virtual bool IsReadOnly => true;

        public abstract bool IsSynchronized { get; }

        public abstract object SyncRoot { get; }

        public abstract ICollection Keys { get; }

        public abstract ICollection Values { get; }

        public virtual object this[object k]
        {
            get => GetValue(k);
            set => throw new NotSupportedException();
        }

        protected abstract object GetValue(object k);
    }
}