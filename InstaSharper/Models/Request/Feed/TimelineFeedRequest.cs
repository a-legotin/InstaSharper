using System.Collections.Generic;
using InstaSharper.API.Services;
using InstaSharper.Models.Request.User;
using InstaSharper.Utils;

namespace InstaSharper.Models.Request.Feed;

internal sealed class TimelineFeedRequest : GetRequestBase
{
    private TimelineFeedRequest(Dictionary<string, string> data)
    {
        RequestData = data;
    }

    public static GetRequestBase Build(IApiStateProvider apiStateProvider,
                                       string sessionId,
                                       string clientSessionId,
                                       string nextMaxId)
    {
        var data = new Dictionary<string, string>
        {
            { "is_prefetch", "0" },
            { "_uuid", apiStateProvider.Device.DeviceId.ToString() },
            { "device_id", apiStateProvider.Device.DeviceId.ToString() },
            { "phone_id", apiStateProvider.Device.PhoneId.ToString() },
            { "client_session_id", clientSessionId },
            { "session_id", sessionId },
            { "timezone_offset", Constants.TIMEZONE_OFFSET.ToString() },
            { "battery_level", "100" },
            { "is_charging", "0" },
            { "rti_delivery_backend", "0" },
            { "is_async_ads_double_request", "0" },
            { "is_async_ads_rti", "0" },
            { "will_sound_on", "0" }
        };

        if (string.IsNullOrEmpty(nextMaxId))
        {
            data.Add("reason", "cold_start_fetch");
        }
        else
        {
            data.Add("reason", "pagination");
            data.Add("max_id", nextMaxId);
        }

        data.Add("is_pull_to_refresh", "0");

        return new TimelineFeedRequest(data);
    }
}