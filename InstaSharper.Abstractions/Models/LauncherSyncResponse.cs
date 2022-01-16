using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models;

public class LauncherSyncResponse
{
    public string PublicKey { get; set; }

    public string KeyId { get; set; }
    public string ShbId { get; set; }
    public string ShbTs { get; set; }
    public string Rur { get; set; }
}

public class PaginationParameters
{
    private PaginationParameters()
    {
    }

    public string RankToken { get; set; } = string.Empty;

    public string NextMaxId { get; set; } = string.Empty;

    public string NextMinId { get; set; } = string.Empty;

    public int MaximumPagesToLoad { get; set; }

    public int PagesLoaded { get; set; } = 1;

    public int? NextPage { get; set; }

    public static PaginationParameters Empty => MaxPagesToLoad(int.MaxValue);

    public static PaginationParameters MaxPagesToLoad(int maxPagesToLoad)
    {
        return new()
        {
            MaximumPagesToLoad = maxPagesToLoad
        };
    }

    public PaginationParameters StartFromMaxId(string maxId)
    {
        NextMaxId = maxId;
        NextMinId = null;
        return this;
    }

    public PaginationParameters StartFromMinId(string minId)
    {
        NextMinId = minId;
        NextMaxId = null;
        return this;
    }

    public PaginationParameters StartFromRankToken(
        string nextId,
        string rankToken)
    {
        NextMaxId = nextId;
        RankToken = rankToken;
        return this;
    }
}

public interface IInstaList<T> : ICollection<T>
{
    string NextMaxId { get; set; }
}