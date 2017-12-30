using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.API.UriCreators;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Converters;
using InstaSharper.Helpers;
using InstaSharper.Logger;
using Newtonsoft.Json;

namespace InstaSharper.API.Processors
{
    internal class LocationProcessor : ILocationProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly IUriCreator _searchLocationUriCreator = new SearchLocationUriCreator();
        private readonly UserSessionData _user;

        public LocationProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
        }

        public async Task<IResult<InstaLocationShortList>> Search(double latitude, double longitude, string query)
        {
            try
            {
                var uri = _searchLocationUriCreator.GetUri();

                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"latitude", latitude.ToString(CultureInfo.InvariantCulture)},
                    {"longitude", longitude.ToString(CultureInfo.InvariantCulture)},
                    {"rank_token", _user.RankToken}
                };

                if (!string.IsNullOrEmpty(query))
                    fields.Add("search_query", query);
                else
                    fields.Add("timestamp", DateTimeHelper.GetUnixTimestampSeconds().ToString());
                if(!Uri.TryCreate(uri, fields.AsQueryString(), out var newuri))
                    return Result.Fail<InstaLocationShortList>("Unable to create uri for location search");

                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, newuri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaLocationShortList>(response, json);
                var locations = JsonConvert.DeserializeObject<InstaLocationSearchResponse>(json);
                var converter = ConvertersFabric.Instance.GetLocationsSearchConverter(locations);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                return Result.Fail<InstaLocationShortList>(exception);
            }
        }
    }
}