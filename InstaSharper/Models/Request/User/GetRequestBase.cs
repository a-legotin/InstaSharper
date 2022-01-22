using System.Collections.Generic;

namespace InstaSharper.Models.Request.User;

internal abstract class GetRequestBase
{
    protected internal virtual IDictionary<string, object> Headers { get; protected set; } =
        new Dictionary<string, object>();

    protected internal virtual IDictionary<string, string> RequestData { get; protected set; } =
        new Dictionary<string, string>();
}