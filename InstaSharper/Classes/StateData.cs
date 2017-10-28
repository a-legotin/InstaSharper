using System;
using System.Collections.Generic;
using System.Text;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Logger;

namespace InstaSharper.Classes
{
    [Serializable]
    public class StateData
    {
        public StateData() { }
        public AndroidDevice DeviceInfo { get; set; }
        public UserSessionData UserSession { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
