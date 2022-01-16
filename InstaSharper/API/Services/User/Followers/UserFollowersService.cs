﻿using System;
using System.Threading.Tasks;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Http;
using InstaSharper.Infrastructure.Converters;
using InstaSharper.Models.Response.User;
using InstaSharper.Models.User;
using LanguageExt;

namespace InstaSharper.API.Services.Followers;

internal class UserFollowersService : IFollowersService
{
    private readonly IInstaHttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly IUserFollowersUriProvider _uriProvider;
    private readonly IUserConverters _userConverters;

    public UserFollowersService(IInstaHttpClient httpClient,
        IUserFollowersUriProvider uriProvider,
        IUserConverters userConverters,
        ILogger logger)
    {
        _httpClient = httpClient;
        _uriProvider = uriProvider;
        _userConverters = userConverters;
        _logger = logger;
    }

    public async Task<Either<ResponseStatusBase, IInstaList<InstaUserShort>>> GetUserFollowersAsync(
        long userPk,
        PaginationParameters paginationParameters)
    {
        paginationParameters ??= PaginationParameters.MaxPagesToLoad(1);
        var rankToken = Guid.NewGuid();
        var userFollowersUri =
            _uriProvider.GetUserFollowersUri(userPk, rankToken.ToString(), paginationParameters.NextMaxId);

        return (await GetUserListByUriAsync(userFollowersUri))
            .Map(response => (IInstaList<InstaUserShort>)new InstaUserShortList());
    }

    private async Task<Either<ResponseStatusBase, InstaUserListShortResponse>> GetUserListByUriAsync(Uri uri)
    {
        return await _httpClient.GetAsync<InstaUserListShortResponse>(uri);
    }
}