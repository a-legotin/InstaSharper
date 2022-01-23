using System;
using System.Linq;
using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Http;
using InstaSharper.Infrastructure;
using InstaSharper.Infrastructure.Converters;
using InstaSharper.Models.Response.User;
using InstaSharper.Models.User;
using LanguageExt;

namespace InstaSharper.API.Services.User.Followers;

internal class UserFollowersService : IFollowersService
{
    private readonly IApiStateProvider _apiStateProvider;
    private readonly IInstaHttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly IUserFollowersUriProvider _uriProvider;
    private readonly IUserConverters _userConverters;

    public UserFollowersService(IInstaHttpClient httpClient,
                                IUserFollowersUriProvider uriProvider,
                                IUserConverters userConverters,
                                IApiStateProvider apiStateProvider,
                                ILogger logger)
    {
        _httpClient = httpClient;
        _uriProvider = uriProvider;
        _userConverters = userConverters;
        _apiStateProvider = apiStateProvider;
        _logger = logger;
    }

    public async Task<Either<ResponseStatusBase, IInstaList<InstaUserShort>>> GetUserFollowersAsync(
        long userPk,
        PaginationParameters paginationParameters)
    {
        return await GetUserPreviewsByUri(userPk, paginationParameters, _uriProvider.GetUserFollowersUri);
    }

    public async Task<Either<ResponseStatusBase, IInstaList<InstaUserShort>>> GetUserFollowingAsync(
        long userPk,
        PaginationParameters paginationParameters)
    {
        return await GetUserPreviewsByUri(userPk, paginationParameters, _uriProvider.GetUserFollowingUri);
    }

    private async Task<Either<ResponseStatusBase, IInstaList<InstaUserShort>>> GetUserPreviewsByUri(long userPk,
        PaginationParameters paginationParameters,
        Func<long, string, string, Uri> getUriFunc)
    {
        return await (await _httpClient.GetAsync<InstaUserListShortResponse>(getUriFunc(userPk,
                _apiStateProvider.RankToken,
                paginationParameters.NextMaxId)))
            .MapAsync(async r
                    =>
                {
                    paginationParameters.PagesLoaded++;
                    paginationParameters.NextMaxId = r.NextMaxId;
                    IInstaList<InstaUserShort> users =
                        new InstaUserShortList(r.Items.Select(_userConverters.Self.Convert));

                    while (paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad
                           && !string.IsNullOrEmpty(paginationParameters.NextMaxId))
                        (await _httpClient.GetAsync<InstaUserListShortResponse>(getUriFunc(userPk,
                                _apiStateProvider.RankToken, paginationParameters.NextMaxId)))
                            .Match(ok =>
                            {
                                paginationParameters.PagesLoaded++;
                                users.NextMaxId = paginationParameters.NextMaxId = ok.NextMaxId;
                                users.AddRange(ok.Items.Select(_userConverters.Self.Convert));
                            }, fail => { });

                    return users;
                }
            );
    }
}