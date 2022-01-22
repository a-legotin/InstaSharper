using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models;

public interface IInstaList<T> : ICollection<T>
{
    string NextMaxId { get; set; }
}