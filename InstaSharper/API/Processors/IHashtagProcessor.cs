using System.Collections.Generic;
using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;

namespace InstaSharper.API.Processors
{
    public interface IHashtagProcessor
    {
        Task<IResult<InstaHashtagSearch>> Search(string query, IEnumerable<long> excludeList = null, string rankToken = null);

        Task<IResult<InstaHashtag>> GetHashtagInfo(string tagname);
    }
}