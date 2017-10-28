using System;
using System.Net;
using InstaSharper.Classes.Android.DeviceInfo;

namespace InstaSharper.Classes
{
    [Serializable]
    internal class StateData
    {
        public AndroidDevice DeviceInfo { get; set; }
        public UserSessionData UserSession { get; set; }
        public bool IsAuthenticated { get; set; }
        public CookieContainer Cookies { get; set; }
    }
}