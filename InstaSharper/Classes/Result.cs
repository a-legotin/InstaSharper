using System;

namespace InstaSharper.Classes
{
    public class Result<T> : IResult<T>
    {
        public Result(bool succeeded, T value, ResultInfo info)
        {
            Succeeded = succeeded;
            Value = value;
            Info = info;
        }

        public Result(bool succeeded, ResultInfo info)
        {
            Succeeded = succeeded;
            Info = info;
        }

        public Result(bool succeeded, T value)
        {
            Succeeded = succeeded;
            Value = value;
        }

        public bool Succeeded { get; }
        public T Value { get; }
        public ResultInfo Info { get; } = new ResultInfo("");
    }

    public static class Result
    {
        public static IResult<T> Success<T>(T resValue)
            => new Result<T>(true, resValue);

        public static IResult<T> Success<T>(string successMsg, T resValue)
            => new Result<T>(true, resValue, new ResultInfo(successMsg));

        public static IResult<T> Fail<T>(Exception exception)
            => new Result<T>(false, default(T), new ResultInfo(exception));

        public static IResult<T> Fail<T>(string errMsg)
            => new Result<T>(false, default(T), new ResultInfo(errMsg));

        public static IResult<T> Fail<T>(string errMsg, T resValue)
            => new Result<T>(false, resValue, new ResultInfo(errMsg));

        public static IResult<T> Fail<T>(string errMsg, ResponseType responseType, T resValue)
            => new Result<T>(false, resValue, new ResultInfo(responseType));
    }
}