using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Converters;
using InstaSharper.Converters.Json;
using InstaSharper.Helpers;
using InstaSharper.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstaSharper.API.Processors
{
    internal class MediaProcessor : IMediaProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;

        public MediaProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
        }

        public async Task<IResult<string>> GetMediaIdFromUrlAsync(Uri uri)
        {
            try
            {
                var collectionUri = UriCreator.GetMediaIdFromUrlUri(uri);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, collectionUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<string>(response, json);

                var data = JsonConvert.DeserializeObject<InstaOembedUrlResponse>(json);
                return Result.Success(data.MediaId);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<string>(exception);
            }
        }

        public async Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType)
        {
            try
            {
                var deleteMediaUri = UriCreator.GetDeleteMediaUri(mediaId, mediaType);

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"media_id", mediaId}
                };

                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Get, deleteMediaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);

                var deletedResponse = JsonConvert.DeserializeObject<DeleteResponse>(json);
                return Result.Success(deletedResponse.IsDeleted);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }

        public async Task<IResult<bool>> EditMediaAsync(string mediaId, string caption)
        {
            try
            {
                var editMediaUri = UriCreator.GetEditMediaUri(mediaId);

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"caption_text", caption}
                };

                var request = HttpHelper.GetSignedRequest(HttpMethod.Get, editMediaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    return Result.Success(true);
                var error = JsonConvert.DeserializeObject<BadStatusResponse>(json);
                return Result.Fail(error.Message, false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }

        public async Task<IResult<InstaMedia>> UploadPhotoAsync(InstaImage image, string caption)
        {
            try
            {
                var instaUri = UriCreator.GetUploadPhotoUri();
                var uploadId = ApiRequestMessage.GenerateUploadId();
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(uploadId), "\"upload_id\""},
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {
                        new StringContent("{\"lib_name\":\"jt\",\"lib_version\":\"1.3.0\",\"quality\":\"87\"}"),
                        "\"image_compression\""
                    }
                };
                var imageContent = new ByteArrayContent(File.ReadAllBytes(image.URI));
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                requestContent.Add(imageContent, "photo", $"pending_media_{ApiRequestMessage.GenerateUploadId()}.jpg");
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return await ConfigurePhotoAsync(image, uploadId, caption);
                return Result.UnExpectedResponse<InstaMedia>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        public async Task<IResult<InstaMedia>> UploadPhotosAlbumAsync(InstaImage[] images, string caption)
        {
            try
            {
                var uploadIds = new string[images.Length];
                var index = 0;

                foreach (var image in images)
                {
                    var instaUri = UriCreator.GetUploadPhotoUri();
                    var uploadId = ApiRequestMessage.GenerateUploadId();
                    var requestContent = new MultipartFormDataContent(uploadId)
                    {
                        {new StringContent(uploadId), "\"upload_id\""},
                        {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                        {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                        {
                            new StringContent("{\"lib_name\":\"jt\",\"lib_version\":\"1.3.0\",\"quality\":\"87\"}"),
                            "\"image_compression\""
                        },
                        {new StringContent("1"), "\"is_sidecar\""}
                    };
                    var imageContent = new ByteArrayContent(File.ReadAllBytes(image.URI));
                    imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                    imageContent.Headers.Add("Content-Type", "application/octet-stream");
                    requestContent.Add(imageContent, "photo",
                        $"pending_media_{ApiRequestMessage.GenerateUploadId()}.jpg");
                    var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                    request.Content = requestContent;
                    var response = await _httpRequestProcessor.SendAsync(request);
                    var json = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                        uploadIds[index++] = uploadId;
                    else
                        return Result.UnExpectedResponse<InstaMedia>(response, json);
                }

                return await ConfigureAlbumAsync(uploadIds, caption);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        public async Task<IResult<InstaMedia>> ConfigurePhotoAsync(InstaImage image, string uploadId, string caption)
        {
            try
            {
                var instaUri = UriCreator.GetMediaConfigureUri();
                var androidVersion =
                    AndroidVersion.FromString(_deviceInfo.FirmwareFingerprint.Split('/')[2].Split(':')[1]);
                if (androidVersion == null)
                    return Result.Fail("Unsupported android version", (InstaMedia) null);
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"media_folder", "Camera"},
                    {"source_type", "4"},
                    {"caption", caption},
                    {"upload_id", uploadId},
                    {
                        "device", new JObject
                        {
                            {"manufacturer", _deviceInfo.HardwareManufacturer},
                            {"model", _deviceInfo.HardwareModel},
                            {"android_version", androidVersion.VersionNumber},
                            {"android_release", androidVersion.APILevel}
                        }
                    },
                    {
                        "edits", new JObject
                        {
                            {"crop_original_size", new JArray {image.Width, image.Height}},
                            {"crop_center", new JArray {0.0, -0.0}},
                            {"crop_zoom", 1}
                        }
                    },
                    {
                        "extra", new JObject
                        {
                            {"source_width", image.Width},
                            {"source_height", image.Height}
                        }
                    }
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    return Result.UnExpectedResponse<InstaMedia>(response, json);
                var mediaResponse =
                    JsonConvert.DeserializeObject<InstaMediaItemResponse>(json, new InstaMediaDataConverter());
                var converter = ConvertersFabric.Instance.GetSingleMediaConverter(mediaResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        public async Task<IResult<InstaMedia>> ConfigureAlbumAsync(string[] uploadId, string caption)
        {
            try
            {
                var instaUri = UriCreator.GetMediaAlbumConfigureUri();
                var clientSidecarId = ApiRequestMessage.GenerateUploadId();

                var childrenArray = new JArray();

                foreach (var id in uploadId)
                    childrenArray.Add(new JObject
                    {
                        {"scene_capture_type", "standard"},
                        {"mas_opt_in", "NOT_PROMPTED"},
                        {"camera_position", "unknown"},
                        {"allow_multi_configures", false},
                        {"geotag_enabled", false},
                        {"disable_comments", false},
                        {"source_type", 0},
                        {"upload_id", id}
                    });

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"caption", caption},
                    {"client_sidecar_id", clientSidecarId},
                    {"geotag_enabled", false},
                    {"disable_comments", false},
                    {"children_metadata", childrenArray}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    return Result.UnExpectedResponse<InstaMedia>(response, json);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaItemResponse>(json);
                var converter = ConvertersFabric.Instance.GetSingleMediaConverter(mediaResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        public async Task<IResult<InstaCommentList>> GetMediaCommentsAsync(string mediaId,
            PaginationParameters paginationParameters)
        {
            try
            {
                var commentsUri = UriCreator.GetMediaCommentsUri(mediaId, paginationParameters.NextId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaCommentList>(response, json);
                var commentListResponse = JsonConvert.DeserializeObject<InstaCommentListResponse>(json);
                var pagesLoaded = 1;

                InstaCommentList Convert(InstaCommentListResponse commentsResponse)
                {
                    return ConvertersFabric.Instance.GetCommentListConverter(commentsResponse).Convert();
                }

                while (commentListResponse.MoreComentsAvailable
                       && !string.IsNullOrEmpty(commentListResponse.NextMaxId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextComments = await GetCommentListWithMaxIdAsync(mediaId, commentListResponse.NextMaxId);
                    if (!nextComments.Succeeded)
                        Result.Success($"Not all pages was downloaded: {nextComments.Info.Message}",
                            Convert(commentListResponse));
                    commentListResponse.NextMaxId = nextComments.Value.NextMaxId;
                    commentListResponse.MoreComentsAvailable = nextComments.Value.MoreComentsAvailable;
                    commentListResponse.Comments.AddRange(nextComments.Value.Comments);
                    pagesLoaded++;
                }
                var converter = ConvertersFabric.Instance.GetCommentListConverter(commentListResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCommentList>(exception);
            }
        }

        public async Task<IResult<InstaLikersList>> GetMediaLikersAsync(string mediaId)
        {
            try
            {
                var likers = new InstaLikersList();
                var likersUri = UriCreator.GetMediaLikersUri(mediaId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, likersUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaLikersList>(response, json);
                var mediaLikersResponse = JsonConvert.DeserializeObject<InstaMediaLikersResponse>(json);
                likers.UsersCount = mediaLikersResponse.UsersCount;
                if (mediaLikersResponse.UsersCount < 1) return Result.Success(likers);
                likers.AddRange(
                    mediaLikersResponse.Users.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                return Result.Success(likers);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaLikersList>(exception);
            }
        }

        public async Task<IResult<bool>> LikeMediaAsync(string mediaId)
        {
            try
            {
                return await LikeUnlikeMediaInternal(mediaId, UriCreator.GetLikeMediaUri(mediaId));
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }

        public async Task<IResult<bool>> UnLikeMediaAsync(string mediaId)
        {
            try
            {
                return await LikeUnlikeMediaInternal(mediaId, UriCreator.GetUnLikeMediaUri(mediaId));
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }

        public async Task<IResult<InstaMedia>> GetMediaByIdAsync(string mediaId)
        {
            try
            {
                var mediaUri = UriCreator.GetMediaUri(mediaId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, mediaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMedia>(response, json);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                if (mediaResponse.Medias?.Count > 1)
                {
                    var errorMessage = $"Got wrong media count for request with media id={mediaId}";
                    _logger?.LogInfo(errorMessage);
                    return Result.Fail<InstaMedia>(errorMessage);
                }
                var converter =
                    ConvertersFabric.Instance.GetSingleMediaConverter(mediaResponse.Medias.FirstOrDefault());
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        private async Task<IResult<InstaCommentListResponse>> GetCommentListWithMaxIdAsync(string mediaId,
            string nextId)
        {
            var commentsUri = UriCreator.GetMediaCommentsUri(mediaId, nextId);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.Fail("Unable to get next portion of comments", (InstaCommentListResponse) null);
            var comments = JsonConvert.DeserializeObject<InstaCommentListResponse>(json);
            return Result.Success(comments);
        }

        private async Task<IResult<bool>> LikeUnlikeMediaInternal(string mediaId, Uri instaUri)
        {
            var fields = new Dictionary<string, string>
            {
                {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                {"_uid", _user.LoggedInUder.Pk},
                {"_csrftoken", _user.CsrfToken},
                {"media_id", mediaId}
            };
            var request =
                HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            return response.StatusCode == HttpStatusCode.OK
                ? Result.Success(true)
                : Result.UnExpectedResponse<bool>(response, json);
        }
    }
}