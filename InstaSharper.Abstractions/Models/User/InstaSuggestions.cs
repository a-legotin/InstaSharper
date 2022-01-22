using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.User;

public class InstaSuggestions
{
    public bool MoreAvailable { get; set; }

    public string NextMaxId { get; set; }

    public List<InstaSuggestionItem> SuggestedUsers { get; set; } = new();

    public List<InstaSuggestionItem> NewSuggestedUsers { get; set; } = new();
}