using System;
using System.Collections;

namespace InstaSharper.Utils.Encryption.Engine;

internal abstract class UnmodifiableList
    : IList
{
    public virtual int Add(object o)
    {
        throw new NotSupportedException();
    }

    public virtual void Clear()
    {
        throw new NotSupportedException();
    }

    public abstract bool Contains(object o);

    public abstract void CopyTo(Array array,
                                int index);

    public abstract int Count { get; }

    public abstract IEnumerator GetEnumerator();

    public abstract int IndexOf(object o);

    public virtual void Insert(int i,
                               object o)
    {
        throw new NotSupportedException();
    }

    public abstract bool IsFixedSize { get; }

    public virtual bool IsReadOnly => true;

    public abstract bool IsSynchronized { get; }

    public virtual void Remove(object o)
    {
        throw new NotSupportedException();
    }

    public virtual void RemoveAt(int i)
    {
        throw new NotSupportedException();
    }

    public abstract object SyncRoot { get; }

    public virtual object this[int i]
    {
        get => GetValue(i);
        set => throw new NotSupportedException();
    }

    protected abstract object GetValue(int i);
}