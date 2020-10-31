using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Models.Status;
using LanguageExt;

namespace InstaSharper.Http
{
    internal interface IInstaHttpClient
    {
        Task<Either<ResponseStatusBase, T>> SendAsync<T>(HttpRequestMessage requestMessage);
    }
}