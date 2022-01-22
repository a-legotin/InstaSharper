using System;
using System.Collections;

namespace InstaSharper.Utils.Encryption.Engine;

internal class UnmodifiableSetProxy
    : UnmodifiableSet
{
    private readonly ISet s;

    public UnmodifiableSetProxy(ISet s)
    {
        this.s = s;
    }

    public override int Count => s.Count;

    public override bool IsEmpty => s.IsEmpty;

    public override bool IsFixedSize => s.IsFixedSize;

    public override bool IsSynchronized => s.IsSynchronized;

    public override object SyncRoot => s.SyncRoot;

    public override bool Contains(object o)
    {
        return s.Contains(o);
    }

    public override void CopyTo(Array array,
                                int index)
    {
        s.CopyTo(array, index);
    }

    public override IEnumerator GetEnumerator()
    {
        return s.GetEnumerator();
    }
}