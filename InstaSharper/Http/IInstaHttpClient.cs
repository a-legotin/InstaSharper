using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Models.Request.User;
using LanguageExt;

namespace InstaSharper.Http;

internal interface IInstaHttpClient
{
    Task<Either<ResponseStatusBase, T>> SendAsync<T>(HttpRequestMessage requestMessage);
    Task<Either<ResponseStatusBase, HttpResponseMessage>> SendAsync(HttpRequestMessage requestMessage);

    Task<Either<ResponseStatusBase, T>> PostSignedAsync<T, R>(Uri uri,
                                                              R requestData);

    Task<Either<ResponseStatusBase, HttpResponseMessage>> PostSignedAsync<T>(Uri uri,
                                                                             T requestData);

    Task<Either<ResponseStatusBase, T>> PostUnsignedAsync<T>(Uri uri,
                                                             GetRequestBase request);

    Task<Either<ResponseStatusBase, T>> GetAsync<T>(Uri uri,
                                                    GetRequestBase requestData);

    Task<Either<ResponseStatusBase, T>> GetAsync<T>(Uri uri);
}