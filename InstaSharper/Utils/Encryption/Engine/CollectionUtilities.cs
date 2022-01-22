using System;
using System.Collections;
using System.Text;

namespace InstaSharper.Utils.Encryption.Engine;

internal abstract class CollectionUtilities
{
    public static void AddRange(IList to,
                                IEnumerable range)
    {
        foreach (var o in range) to.Add(o);
    }

    public static bool CheckElementsAreOfType(IEnumerable e,
                                              Type t)
    {
        foreach (var o in e)
            if (!t.IsInstanceOfType(o))
                return false;

        return true;
    }

    public static IDictionary ReadOnly(IDictionary d)
    {
        return new UnmodifiableDictionaryProxy(d);
    }

    public static IList ReadOnly(IList l)
    {
        return new UnmodifiableListProxy(l);
    }

    public static ISet ReadOnly(ISet s)
    {
        return new UnmodifiableSetProxy(s);
    }

    public static object RequireNext(IEnumerator e)
    {
        if (!e.MoveNext())
            throw new InvalidOperationException();

        return e.Current;
    }

    public static string ToString(IEnumerable c)
    {
        var e = c.GetEnumerator();
        if (!e.MoveNext())
            return "[]";

        var sb = new StringBuilder("[");
        sb.Append(e.Current);
        while (e.MoveNext())
        {
            sb.Append(", ");
            sb.Append(e.Current);
        }

        sb.Append(']');
        return sb.ToString();
    }
}