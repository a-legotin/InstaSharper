using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstaSharper.Abstractions.Logging
{
    public interface ILogger
    {
        Task LogRequest(HttpRequestMessage request);

        void LogRequest(Uri uri);

        Task LogResponse(HttpResponseMessage response);

        void LogException(Exception exception);

        void LogInfo(string info);
    }
}