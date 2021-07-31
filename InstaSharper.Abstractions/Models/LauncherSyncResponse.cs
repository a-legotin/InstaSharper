using System.Collections;
using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models
{
    public class LauncherSyncResponse
    {
        public string PublicKey { get; set; }

        public string KeyId { get; set; }
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

        public static PaginationParameters Empty => PaginationParameters.MaxPagesToLoad(int.MaxValue);

        public static PaginationParameters MaxPagesToLoad(int maxPagesToLoad) => new PaginationParameters()
        {
            MaximumPagesToLoad = maxPagesToLoad
        };

        public PaginationParameters StartFromMaxId(string maxId)
        {
            this.NextMaxId = maxId;
            this.NextMinId = (string) null;
            return this;
        }

        public PaginationParameters StartFromMinId(string minId)
        {
            this.NextMinId = minId;
            this.NextMaxId = (string) null;
            return this;
        }

        public PaginationParameters StartFromRankToken(
            string nextId,
            string rankToken)
        {
            this.NextMaxId = nextId;
            this.RankToken = rankToken;
            return this;
        }
    }
    
    public interface IInstaList<T> : ICollection<T>
    {
        string NextMaxId { get; set; }
    }
}