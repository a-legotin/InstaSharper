using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models;

public interface IInstaList<T> : IList<T>
{
    string NextMaxId { get; set; }
    void AddRange(IEnumerable<T> items);
}