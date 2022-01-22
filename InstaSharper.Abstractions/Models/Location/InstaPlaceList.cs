using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.Location;

public class InstaPlaceList
{
    public List<InstaPlace> Items { get; set; } = new();

    public bool HasMore { get; set; }

    public string RankToken { get; set; }

    internal string Status { get; set; }

    public List<long> ExcludeList { get; set; } = new();
}