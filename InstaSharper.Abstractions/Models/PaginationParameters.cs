namespace InstaSharper.Abstractions.Models;

public class PaginationParameters
{
    private PaginationParameters()
    {
    }

    public string RankToken { get; set; } = string.Empty;

    public string NextMaxId { get; set; } = string.Empty;

    public string NextMinId { get; set; } = string.Empty;

    public int MaximumPagesToLoad { get; set; }

    public int PagesLoaded { get; set; } = 0;

    public int? NextPage { get; set; }

    public static PaginationParameters AllPages => MaxPagesToLoad(int.MaxValue);

    public static PaginationParameters MaxPagesToLoad(int maxPagesToLoad)
    {
        return new PaginationParameters
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