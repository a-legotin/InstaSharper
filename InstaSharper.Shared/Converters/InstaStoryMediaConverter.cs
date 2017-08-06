﻿using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaStoryMediaConverter : IObjectConverter<InstaStoryMedia, InstaStoryMediaResponse>
    {
        public InstaStoryMediaResponse SourceObject { get; set; }

        public InstaStoryMedia Convert()
        {
            var instaStoryMedia = new InstaStoryMedia
            {
                Media = ConvertersFabric.GetStoryItemConverter(SourceObject.Media).Convert()
            };

            return instaStoryMedia;
        }
    }
}