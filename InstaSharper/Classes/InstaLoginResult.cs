using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes
{
    public enum InstaLoginResult
    {
        Success,
        BadPassword,
        InvalidUser,
        TwoFactorRequired,
        Exception
    }
}
