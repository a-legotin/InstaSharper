using System;
using System.Collections;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal class UnmodifiableListProxy
        : UnmodifiableList
    {
        private readonly IList l;

        public UnmodifiableListProxy(IList l) => this.l = l;

        public override int Count => l.Count;

        public override bool IsFixedSize => l.IsFixedSize;

        public override bool IsSynchronized => l.IsSynchronized;

        public override object SyncRoot => l.SyncRoot;

        public override bool Contains(object o) => l.Contains(o);

        public override void CopyTo(Array array, int index)
        {
            l.CopyTo(array, index);
        }

        public override IEnumerator GetEnumerator() => l.GetEnumerator();

        public override int IndexOf(object o) => l.IndexOf(o);

        protected override object GetValue(int i) => l[i];
    }
}