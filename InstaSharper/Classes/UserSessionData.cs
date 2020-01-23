﻿using System;
using InstaSharper.Classes.Models;

namespace InstaSharper.Classes
{
    [Serializable]
    public class UserSessionData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public InstaUserShort LoggedInUser { get; set; }

        public string RankToken { get; set; }
        public string CsrfToken { get; set; }

        public static UserSessionData Empty => new UserSessionData();

        public static UserSessionData ForUsername(string username)
        {
            return new UserSessionData {UserName = username};
        }

        public UserSessionData WithPassword(string password)
        {
            Password = password;
            return this;
        }
    }
}