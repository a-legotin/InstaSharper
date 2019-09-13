﻿using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Helpers;

namespace InstaSharper.Converters
{
    internal class InstaReelFeedConverter : IObjectConverter<InstaReelFeed, InstaReelFeedResponse>
    {
        public InstaReelFeedResponse SourceObject { get; set; }

        public InstaReelFeed Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var reelFeed = new InstaReelFeed
            {
                CanReply = SourceObject.CanReply,
                CanReshare = SourceObject.CanReshare,
                ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject?.ExpiringAt ?? 0),
                HasBestiesMedia = SourceObject.HasBestiesMedia,
                Id = SourceObject.Id,
                LatestReelMedia = SourceObject.LatestReelMedia ?? 0,
                PrefetchCount = SourceObject.PrefetchCount,
                Seen = SourceObject.Seen ?? 0,
                User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert()
            };

            if (SourceObject.Items != null)
                foreach (var item in SourceObject.Items)
                    reelFeed.Items.Add(ConvertersFabric.Instance.GetStoryItemConverter(item).Convert());
            return reelFeed;
        }
    }
}