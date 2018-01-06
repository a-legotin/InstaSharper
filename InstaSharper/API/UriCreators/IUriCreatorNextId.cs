using System;

namespace InstaSharper.API.UriCreators
{
    public interface IUriCreatorNextId
    {
        Uri GetUri(long id, string nextId);
    }
}