using System;

namespace InstaSharper.Classes
{
    public class ResultInfo
    {
        public ResultInfo(string message)
        {
            Message = message;
        }

        public ResultInfo(Exception exception)
        {
            Exception = exception;
        }

        public ResultInfo(ResponseType responseType, string errorMessage)
        {
            ResponseType = responseType;
            Message = errorMessage;
        }

        public Exception Exception { get; }

        public string Message { get; }

        public ResponseType ResponseType { get; }

        public override string ToString()
        {
            var message = $"{ResponseType.ToString()}: {Message}.";
            if (Exception != null) message += $"Exception: {Exception.Message}";
            return message;
        }
    }
}
