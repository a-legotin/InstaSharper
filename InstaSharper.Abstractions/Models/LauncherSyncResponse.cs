namespace InstaSharper.Abstractions.Models;

public class LauncherSyncResponse
{
    public string PublicKey { get; set; }

    public string KeyId { get; set; }
    public string ShbId { get; set; }
    public string ShbTs { get; set; }
    public string Rur { get; set; }
}