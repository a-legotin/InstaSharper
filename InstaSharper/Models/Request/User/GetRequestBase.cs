using System.Collections.Generic;

namespace InstaSharper.Models.Request.User
{
    internal abstract class GetRequestBase
    {
        protected internal virtual Dictionary<string, object> Headers { get; } = new Dictionary<string, object>();
    }
}