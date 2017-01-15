using System;

namespace InstaSharper.Classes
{
    public static class Result
    {
        public static IResult<T> Success<T>(T resValue)
            => new Result<T>(true, string.Empty, resValue, null);

        public static IResult<T> Success<T>(string successMsg, T resValue)
            => new Result<T>(true, successMsg, resValue, null);

        public static IResult<object> Fail()
            => new Result<object>(false, string.Empty, default(object), null);

        public static IResult<object> Fail(Exception exception)
            => new Result<object>(false, string.Empty, default(object), exception);

        public static IResult<T> Fail<T>()
            => new Result<T>(false, string.Empty, default(T), null);

        public static IResult<T> Fail<T>(Exception exception)
            => new Result<T>(false, string.Empty, default(T), exception);

        public static IResult<T> Fail<T>(T resValue)
            => new Result<T>(false, string.Empty, resValue, null);

        public static IResult<object> Fail(string errMsg)
            => new Result<object>(false, errMsg, default(object), null);

        public static IResult<T> Fail<T>(string errMsg)
            => new Result<T>(false, errMsg, default(T), null);

        public static IResult<T> Fail<T>(string errMsg, T resValue)
            => new Result<T>(false, errMsg, resValue, null);

        public static T GetValueOrDefault<T>(this IResult<T> result, Action<string> logAction = null)
        {
            if (result.Succeeded) return result.Value;

            if (!string.IsNullOrEmpty(result.Message)) logAction?.Invoke(result.Message);
            return default(T);
        }
    }

    internal struct Result<T> : IResult<T>
    {
        public bool Succeeded { get; }
        public string Message { get; }
        public T Value { get; }

        public Exception Exception { get; }

        public Result(bool succeeded, string errorMessage, T value, Exception exception)
        {
            Succeeded = succeeded;
            Message = errorMessage;
            Value = value;
            Exception = exception;
        }
    }
}