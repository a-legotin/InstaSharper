using System;

namespace InstaSharper.Abstractions.Models.User;

[Serializable]
public class InstaUserShort
{
    public bool IsVerified { get; set; }

    public bool IsPrivate { get; set; }

    public long Pk { get; set; }

    public string ProfilePicture { get; set; }

    public string ProfilePicUrl { get; set; }

    public string ProfilePictureId { get; set; } = "unknown";

    public string UserName { get; set; }

    public string FullName { get; set; }

    public bool HasAnonymousProfilePicture { get; set; }

    public static InstaUserShort Empty => new()
    {
        FullName = string.Empty,
        UserName = string.Empty
    };

    public bool Equals(InstaUserShort user)
    {
        var pk1 = Pk;
        var pk2 = user?.Pk;
        var valueOrDefault = pk2.GetValueOrDefault();
        return (pk1 == valueOrDefault) & pk2.HasValue;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as InstaUserShort);
    }

    public override int GetHashCode()
    {
        return Pk.GetHashCode();
    }
}