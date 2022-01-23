using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models;

public interface IInstaList<T> : IList<T>
{
    void AddRange(IEnumerable<T> items);
    string NextMaxId { get; set; }
}