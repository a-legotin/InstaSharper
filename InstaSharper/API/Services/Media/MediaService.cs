using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Http;
using InstaSharper.Infrastructure.Converters;
using InstaSharper.Models.Response.Media;
using LanguageExt;

namespace InstaSharper.API.Services.Media;

internal class MediaService : IMediaService
{
    private readonly IDevice _device;
    private readonly IInstaHttpClient _httpClient;
    private readonly IMediaConverters _mediaConverters;
    private readonly IMediaUriProvider _mediaUriProvider;

    public MediaService(IInstaHttpClient httpClient,
                        IMediaUriProvider mediaUriProvider,
                        IDevice device,
                        IMediaConverters mediaConverters)
    {
        _httpClient = httpClient;
        _mediaUriProvider = mediaUriProvider;
        _device = device;
        _mediaConverters = mediaConverters;
    }

    public async Task<Either<ResponseStatusBase, InstaMediaList>> GetMediaListByIdsAsync(params string[] mediaIds)
    {
        return mediaIds?.Length < 1
            ? new InstaMediaList()
            : (await _httpClient.GetAsync<InstaMediaListResponse>(
                _mediaUriProvider.GetMediaInfosUri(_device.DeviceId.ToString(), mediaIds)))
            .Map(ok => _mediaConverters.List.Convert(ok));
    }
}