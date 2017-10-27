using System;
using System.Threading.Tasks;

namespace InstaSharper.Logger
{
    public interface ILogger
    {
        void OnRequest(object request);
        void OnResponse(object response);
        void OnError(Exception exception);
        void OnInfo(string info);
    }
}