using System;
using System.Linq;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaHashtagSearchConverter : IObjectConverter<InstaHashtagSearch, InstaHashtagSearchResponse>
    {
        public InstaHashtagSearchResponse SourceObject { get; set; }

        public InstaHashtagSearch Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException($"Source object");

            var tags = new InstaHashtagSearch();

            tags.MoreAvailable = SourceObject.MoreAvailable.GetValueOrDefault(false);
            tags.RankToken = SourceObject.RankToken;
            tags.AddRange(SourceObject.Tags.Select(tag =>
                ConvertersFabric.Instance.GetHashTagConverter(tag).Convert()));

            return tags;
        }
    }
}
