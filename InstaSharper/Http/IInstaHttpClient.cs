using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Models.Status;
using LanguageExt;

namespace InstaSharper.Http
{
    internal interface IInstaHttpClient
    {
        Task<Either<ResponseStatusBase, T>> SendAsync<T>(HttpRequestMessage requestMessage);
        Task<Either<ResponseStatusBase, HttpResponseMessage>> SendAsync(HttpRequestMessage requestMessage);
        Task<Either<ResponseStatusBase, T>> PostAsync<T, R>(Uri uri, R requestData);
        Task<Either<ResponseStatusBase, HttpResponseMessage>> PostAsync<T>(Uri uri, T requestData);
    }
}