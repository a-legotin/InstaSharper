﻿using System.Collections.Generic;
using System.Linq;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    public class InstaCollectionsConverter : IObjectConverter<InstaCollections, InstaCollectionsResponse>
    {
        public InstaCollectionsResponse SourceObject { get; set; }

        public InstaCollections Convert()
        {
            var instaCollectionList = new List<InstaCollectionItem>();
            instaCollectionList.AddRange(SourceObject.Items.Select(ConvertersFabric.Instance.GetCollectionConverter)
                .Select(converter => converter.Convert()));

            return new InstaCollections
            {
                Items = instaCollectionList,
                MoreCollectionsAvailable = SourceObject.MoreAvailable
            };
        }
    }
}