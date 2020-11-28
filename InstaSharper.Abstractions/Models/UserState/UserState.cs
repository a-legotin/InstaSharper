using System;
using System.Net;
using InstaSharper.Abstractions.Device;

namespace InstaSharper.Abstractions.Models.UserState
{
    [Serializable]
    public class UserState
    {
        public IDevice Device { get; set; }
        public UserSession UserSession { get; set; }
        public bool IsAuthenticated { get; set; }
        public CookieContainer Cookies { get; set; }
    }
}