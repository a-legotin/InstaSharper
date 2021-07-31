using System;
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

namespace InstaSharper.API.Services.Followers
{
    internal class UserFollowersService
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
            string username,
            PaginationParameters paginationParameters)
        {
            paginationParameters ??= PaginationParameters.MaxPagesToLoad(1);
            var userFollowersUri = _uriProvider.GetUserFollowersUri(0, paginationParameters.NextMaxId);

            return (await GetUserListByUriAsync(userFollowersUri))
                .Map(response =>
                {
                    return (IInstaList<InstaUserShort>) new InstaUserShortList();
                    ;
                });
        }

        private async Task<Either<ResponseStatusBase, InstaUserListShortResponse>> GetUserListByUriAsync(Uri uri) =>
            await _httpClient.GetAsync<InstaUserListShortResponse>(uri);
    }
}