using System;

namespace InstaSharper.Classes
{
    public static class Result
    {
        public static IResult<T> Success<T>(T resValue)
            => new Result<T>(true, string.Empty, resValue);

        public static IResult<T> Success<T>(string successMsg, T resValue)
            => new Result<T>(true, successMsg, resValue);

        public static IResult<object> Fail()
            => new Result<object>(false, string.Empty, default(object));

        public static IResult<T> Fail<T>()
            => new Result<T>(false, string.Empty, default(T));

        public static IResult<T> Fail<T>(T resValue)
            => new Result<T>(false, string.Empty, resValue);

        public static IResult<object> Fail(string errMsg)
            => new Result<object>(false, errMsg, default(object));

        public static IResult<T> Fail<T>(string errMsg)
            => new Result<T>(false, errMsg, default(T));

        public static IResult<T> Fail<T>(string errMsg, T resValue)
            => new Result<T>(false, errMsg, resValue);

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

        public Result(bool succeeded, string errorMessage, T value)
        {
            Succeeded = succeeded;
            Message = errorMessage;
            Value = value;
        }
    }
}