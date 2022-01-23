using System.Collections.Generic;
using InstaSharper.Infrastructure;
using InstaSharper.Models.Request.User;
using InstaSharper.Utils;

namespace InstaSharper.Models.Request.Feed;

internal sealed class StoryFeedRequest : GetRequestBase
{
    private static readonly string _capabilities =
        @"%5B%7B%22name%22%3A%22SUPPORTED_SDK_VERSIONS%22%2C%22value%22%3A%22105.0%2C106.0%2C107.0%2C108.0%2C109.0%2C110.0%2C111.0%2C112.0%2C113.0%2C114.0%2C115.0%2C116.0%2C117.0%2C118.0%2C119.0%2C120.0%2C121.0%2C122.0%2C123.0%22%7D%2C%7B%22name%22%3A%22FACE_TRACKER_VERSION%22%2C%22value%22%3A%2214%22%7D%2C%7B%22name%22%3A%22COMPRESSION%22%2C%22value%22%3A%22ETC2_COMPRESSION%22%7D%2C%7B%22name%22%3A%22world_tracker%22%2C%22value%22%3A%22world_tracker_enabled%22%7D%2C%7B%22name%22%3A%22gyroscope%22%2C%22value%22%3A%22gyroscope_enabled%22%7D%5D";

    private static readonly int _defaultPAgeSize = 50;

    private StoryFeedRequest(Dictionary<string, string> data)
    {
        RequestData = data;
    }

    public static GetRequestBase Build(IApiStateProvider apiStateProvider,
                                       string requestId,
                                       string traySessionId,
                                       int pageSize,
                                       string nextMaxId)
    {
        var data = new Dictionary<string, string>
        {
            { "supported_capabilities_new", _capabilities },
            { "_uuid", apiStateProvider.Device.DeviceId.ToString() },
            { "device_id", apiStateProvider.Device.DeviceId.ToString() },
            { "phone_id", apiStateProvider.Device.PhoneId.ToString() },
            { "tray_session_id", traySessionId },
            { "request_id", requestId },
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

        if (pageSize < 1) pageSize = _defaultPAgeSize;

        data.Add("page_size", pageSize.ToString());
        data.Add("is_pull_to_refresh", "0");

        return new StoryFeedRequest(data);
    }
}