using System.Collections.Generic;
using InstaSharper.Infrastructure;
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
            { "_uuid", apiStateProvider.Device.DeviceId.ToString() },
            { "device_id", apiStateProvider.Device.DeviceId.ToString() },
            { "phone_id", apiStateProvider.Device.PhoneId.ToString() },
            { "client_session_id", clientSessionId },
            { "session_id", sessionId },
            { "timezone_offset", Constants.TIMEZONE_OFFSET.ToString() }
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