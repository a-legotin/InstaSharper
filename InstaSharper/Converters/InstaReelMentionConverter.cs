﻿using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaReelMentionConverter : IObjectConverter<InstaReelMention, InstaReelMentionResponse>
    {
        public InstaReelMentionResponse SourceObject { get; set; }

        public InstaReelMention Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var mention = new InstaReelMention
            {
                Height = SourceObject.Height,
                IsPinned = System.Convert.ToBoolean(SourceObject.IsPinned),
                Rotation = SourceObject.Rotation,
                Width = SourceObject.Width,
                X = SourceObject.X,
                Y = SourceObject.Y
            };
            if (SourceObject.Hashtag != null)
                mention.Hashtag = ConvertersFabric.Instance.GetHashTagConverter(SourceObject.Hashtag).Convert();
            if (SourceObject.User != null)
                mention.User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert();
            return mention;
        }
    }
}