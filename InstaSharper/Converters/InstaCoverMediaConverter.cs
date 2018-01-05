﻿using System.Collections.Generic;
using System.Linq;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaCoverMediaConverter : IObjectConverter<InstaCoverMedia, InstaCoverMediaResponse>
    {
        public InstaCoverMediaResponse SourceObject { get; set; }

        public InstaCoverMedia Convert()
        {
            var instaImageList = new List<InstaImage>();

            if (SourceObject.ImageVersions != null)
                instaImageList.AddRange(SourceObject.ImageVersions.Candidates
                    .Select(ConvertersFabric.Instance.GetImageConverter).Select(converter => converter.Convert()));

            return new InstaCoverMedia
            {
                Id = SourceObject.Id,
                ImageVersions = instaImageList,
                MediaType = SourceObject.MediaType,
                OriginalHeight = SourceObject.OriginalHeight,
                OriginalWidth = SourceObject.OriginalWidth
            };
        }
    }
}