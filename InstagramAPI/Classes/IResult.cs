using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Classes
{
    public interface IResult<out T>
    {
        bool Succeeded { get; }
        string Message { get; }
        T Value { get; }
    }
}
