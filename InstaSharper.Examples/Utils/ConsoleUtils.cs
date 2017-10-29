﻿using System;
using InstaSharper.Classes.Models;

namespace InstaSharper.Examples.Utils
{
    public static class ConsoleUtils
    {
        public static void PrintMedia(string header, InstaMedia media, int maxDescriptionLength)
        {
            Console.WriteLine(
                $"{header} [{media.User.UserName}]: {media.Caption?.Text.Truncate(maxDescriptionLength)}, {media.Code}, likes: {media.LikesCount}, multipost: {media.IsMultiPost}");
        }
    }
}