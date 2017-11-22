using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes
{
    public enum InstaLoginTwoFactorResult
    {
        Success, //Ok
        InvalidCode, //sms_code_validation_code_invalid
        CodeExpired, //invalid_nonce
        Exception
    }
}
