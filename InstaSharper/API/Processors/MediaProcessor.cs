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
using System.Diagnostics;
using System.Web;
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

        public async Task<IResult<InstaMedia>> UploadVideoAsync(InstaVideo video, InstaImage imageThumbnail, string caption)
        {
            try
            {
                //POST /api/v1/upload/video/ HTTP/1.1
                //Host: i.instagram.com
                //upload_id=1458984764906&media_type=2&_uuid=ba913fe1-5c1a-4d66-b892-b77d16c3906a&_csrftoken=d6662d21cb381d03bd55ddddd6f76aa1
                var instaUri = UriCreator.GetUploadVideoUri();
                Debug.WriteLine(instaUri);

                var uploadId = ApiRequestMessage.GenerateUploadId();
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent("2"), "\"media_type\""},
                    {new StringContent(uploadId), "\"upload_id\""},
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {
                        new StringContent("{\"lib_name\":\"jt\",\"lib_version\":\"1.3.0\",\"quality\":\"87\"}"),
                        "\"image_compression\""
                    }
                };

                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);

                var videoResponse = JsonConvert.DeserializeObject<VideoUploadJobResponse>(json);
                if (videoResponse == null)
                    return null;



                //POST /api/v1/stage/video/?target=ash3c07&vx_token=101 HTTP/1.1
                //Host: upload.instagram.com
                //Cookie2: $Version=1
                //job: AQBwFJIpMWQ91PomMs3Lbm1zqoRH_SbuaW7-HAInJBIWC_FgL1prmqeAo_EHQ5yS8PEv4NT_FCMJ-fcWEC6Tp7XiTeYqXuRvSw9yysQeMJ4X1LDDU4R3rqWbm_eveAVp9U6tcSdyKgvjChyknirKw1eRl5KHw7WsX6kQoPb0B6lbgTN4ueoZDKXul2ps5ab-0l3Pq3hOsIU6n59tz-Qwz5ppmJK8bpGFqGWGtdp3f8RasXqLqxzGGnqr6VmBal1w6IWqYiqaw-pO_VzNRxDe0MwqcwiKBK7Itm7558H4XjAkgESL88ZroYkZEmOhFY65SxKuLMXfyp01AVTvnXb9vRdU33mGr-BzHtQ-eNIaUm33PxHiw1I-rLvtGz9nFbjjGBE1jqUC22WPQins7eeO1kRKJxsk9iv_C8R076an5djCEKjBTC6dK5TjSLHD3nzcYeB0fzfZjiRQ2VUsIryTXRyg
                //Content-Range: bytes 0-204799/1124814
                //Cookie: csrftoken=152b808d6caa70c7974778d061228dac; mid=VvZXSAABAAFet8OH061XtoTpykzr

                var fileBytes = File.ReadAllBytes(video.Url);
                var first = videoResponse.VideoUploadUrls[0];
                instaUri = new Uri(HttpUtility.UrlDecode(first.Url));
                Debug.WriteLine(instaUri);


                requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {
                        new StringContent("{\"lib_name\":\"jt\",\"lib_version\":\"1.3.0\",\"quality\":\"87\"}"),
                        "\"image_compression\""
                    }
                };


                var videoContent = new ByteArrayContent(fileBytes);
                videoContent.Headers.Add("Content-Transfer-Encoding", "binary");
                videoContent.Headers.Add("Content-Type", "application/octet-stream");
                videoContent.Headers.Add("Content-Disposition", $"attachment; filename=\"{Path.GetFileName(video.Url)}\"");
                requestContent.Add(videoContent);//, "video", $"pending_media_{ApiRequestMessage.GenerateUploadId()}.mp4");
                request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                request.Headers.Host = "upload.instagram.com";
                request.Headers.Add("Cookie2", "$Version=1");
                request.Headers.Add("Session-ID", uploadId);
                request.Headers.Add("job", first.Job);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                //{"result": "deprecated", "configure_delay_ms": "6500", "status": "ok"}

                Debug.WriteLine(json);

                //POST /api/v1/upload/photo/ HTTP/1.1
                //Host: i.instagram.com
                //Content-Type: multipart/form-data; boundary=ba913fe1-5c1a-4d66-b892-b77d16c3906a
                //Content-Length: 384135
                await UploadVideoThumbnailAsync(imageThumbnail, uploadId);
                ////{"upload_id": "1522619662", "xsharing_nonces": {}, "status": "ok"}


                //POST /api/v1/media/configure/?video=1 HTTP/1.1
                //Host: i.instagram.com
                //signed_body=7d4bfdafc9785b1d4607c5de35be98e4c0a0736f594a86e8131716d5c3c3fdf8.{"caption":"","upload_id":"1458985465780","source_type":"3","clips":[{"length":14.975,"source_type":"3","camera_position":"back"}],"poster_frame_index":0,"length":0.00,"audio_muted":false,"filter_type":"0","video_result":"deprecated","extra":{"source_width":960,"source_height":1280},"device":{"manufacturer":"Xiaomi","model":"HM 1SW","android_version":18,"android_release":"4.3"},"_csrftoken":"d6662d21cb381d03bd55ddddd6f76aa1","_uuid":"ba913fe1-5c1a-4d66-b892-b77d16c3906a","_uid":"2959466246"}&ig_sig_key_version=4

                return await ConfigureVideoAsync(video, uploadId, caption);
                //{"message": "Unknown Server Error.", "status": "fail"}
                //{"message": "Transcode error: Not a single valid video stream", "status": "fail"}
                //{"message": "media_needs_reupload", "media": {"upload_id": "1522624246", "device_timestamp": "1522624246"}, "error_title": "Not a single valid video stream", "status": "ok"}
                //{"message": "Please update your Instagram app to continue posting photos", "status": "fail"}


            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        public async Task<IResult<bool>> UploadVideoThumbnailAsync(InstaImage image, string uploadId)
        {
            try
            {
                var instaUri = UriCreator.GetUploadPhotoUri();
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
                requestContent.Add(imageContent, "photo", $"pending_media_{uploadId}.jpg");
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UploadVideoThumbnailAsync");
                Debug.WriteLine(json);
                Debug.WriteLine("");
                Debug.WriteLine("");
                var imgResp = JsonConvert.DeserializeObject<ImageThumbnailResponse>(json);
                if (imgResp.Status.ToLower() == "ok")
                    return Result.Success(true);
                else
                    return Result.Fail<bool>("Could not upload thumbnail");
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }

        public async Task<IResult<InstaMedia>> ConfigureVideoAsync(InstaVideo video, string uploadId, string caption)
        {
            try
            {
                var instaUri = UriCreator.GetMediaVideoConfigureUri();
                instaUri = UriCreator.GetMediaConfigureUri();
                var androidVersion =
                    AndroidVersion.FromString(_deviceInfo.FirmwareFingerprint.Split('/')[2].Split(':')[1]);
                if (androidVersion == null)
                    return Result.Fail("Unsupported android version", (InstaMedia)null);
                var data = new JObject
                {
                    { "caption", caption},
                    {"upload_id", uploadId},
                    { "source_type", "3"},
                    {"camera_position", "unknown"},
                    {
                        "extra", new JObject
                        {
                            {"source_width", video.Width},
                            {"source_height", video.Height}
                        }
                    },
                    {
                        "clips", new JArray{
                            new JObject
                            {
                                {"length", 10.0},
                                //"2018-02-02T19:03:32-0700",
                                {"creation_date", DateTime.Now.ToString("yyyy-dd-MMTh:mm:ss-0fff")},
                                {"source_type", "3"},
                                {"camera_position", "back"}
                            }
                        }
                    },
                    { "poster_frame_index", 0},
                    { "audio_muted", false},
                    { "filter_type", "0"},
                    { "video_result", "deprecated"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.UserName}
                };
                string configure = $"{{\"caption\":\"{caption}\",\"upload_id\":\"{uploadId}\",\"source_type\":\"3\", \"camera_position\":\"unknown\",\"extra\":{{\"source_width\":1280,\"source_height\":720}},\"clips\":[{{\"length\":10.0,\"creation_date\" :\"2018-02-02T19:03:32-0700\",\"source_type\":\"3\",\"camera_position\":\"back\"}}],\"poster_frame_index\":0,\"audio_muted\":false,\"filter_type\":\"0\",\"video_result\":\"deprecated\",\"_csrftoken\":\"{_user.CsrfToken}\",\"_uuid\":\"{_deviceInfo.DeviceGuid.ToString()}\",\"_uid\":\"{_user.UserName}\"}}";
                Debug.WriteLine(configure);

                var jsonData = JsonConvert.SerializeObject(data);
                Debug.WriteLine(jsonData);

                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
                Debug.WriteLine("");
                Debug.WriteLine("");
                if (!response.IsSuccessStatusCode)
                    return Result.UnExpectedResponse<InstaMedia>(response, json);

                var success = await ExposeVideoAsync(uploadId);
                if (success.Succeeded)
                    return Result.Success(success.Value);
                else
                    return Result.Fail<InstaMedia>("Cannot expose media");
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        public async Task<IResult<InstaMedia>> ExposeVideoAsync(string uploadId)
        {
            try
            {
                //POST /api/v1/qe/expose/ HTTP/1.1
                //Host: i.instagram.com
                //signed_body=dc0ec24bb95546a7cc8ddf5d86603a8b5345f8a64b39ffedc62d4d57a8e6fed4.{"_csrftoken":"d6662d21cb381d03bd55ddddd6f76aa1","experiment":"ig_android_profile_contextual_feed","id":"2959466246","_uid":"2959466246","_uuid":"ba913fe1-5c1a-4d66-b892-b77d16c3906a"}&ig_sig_key_version=4

                var instaUri = UriCreator.GetMediaConfigureUri();
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"experiment", "ig_android_profile_contextual_feed"},
                    {"id", _user.LoggedInUder.Pk},
                    {"upload_id", uploadId},

                };
                var jsonData = JsonConvert.SerializeObject(data);
                Debug.WriteLine(jsonData);

                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                //{"message": "Media deleted.", "status": "fail"}
                var jObject = JsonConvert.DeserializeObject<ImageThumbnailResponse>(json);

                if (jObject.Status.ToLower() == "ok")
                {
                    var mediaResponse =
        JsonConvert.DeserializeObject<InstaMediaItemResponse>(json, new InstaMediaDataConverter());
                    var converter = ConvertersFabric.Instance.GetSingleMediaConverter(mediaResponse);

                    return Result.Success(converter.Convert());
                }
                else
                    return Result.Fail<InstaMedia>(jObject.Status);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
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

        public async Task<IResult<Uri>> GetShareLinkFromMediaIdAsync(string mediaId)
        {
            try
            {
                var collectionUri = UriCreator.GetShareLinkFromMediaId(mediaId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, collectionUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<Uri>(response, json);

                var data = JsonConvert.DeserializeObject<InstaPermalinkResponse>(json);
                return Result.Success(new Uri(data.Permalink));
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<Uri>(exception.Message);
            }
        }

        private async Task<IResult<bool>> LikeUnlikeMediaInternal(string mediaId, Uri instaUri)
        {
            var fields = new Dictionary<string, string>
            {
                {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                {"_uid", _user.LoggedInUder.Pk.ToString()},
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