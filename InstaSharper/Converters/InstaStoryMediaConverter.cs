using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Converters
{
    class InstaStoryMediaConverter : IObjectConverter<InstaStoryMedia, InstaStoryMediaResponse>
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
