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
            Message = exception?.Message;
            ResponseType = ResponseType.InternalException;
        }

        public ResultInfo(ResponseType responseType, string errorMessage)
        {
            ResponseRaw = string.Empty;
            ResponseType = responseType;
            Message = errorMessage;
        }
        
        public ResultInfo(ResponseType responseType, string errorMessage, string responseRaw)
        {
            ResponseRaw = responseRaw;
            ResponseType = responseType;
            Message = errorMessage;
        }

        public Exception Exception { get; }

        public string Message { get; }

        public ResponseType ResponseType { get; }
        
        public string ResponseRaw { get; }

        public override string ToString()
        {
            return $"{ResponseType.ToString()}: {Message}.";
        }
    }
}