using System;
using System.Linq;
using InstagramApi.Classes;
using InstagramApi.ResponseWrappers;

namespace InstagramApi.Converters
{
    internal class InstaPostsConverter : IObjectConverter<InstaPostList, InstaResponse>
    {
        public InstaResponse SourceObject { get; set; }

        public InstaPostList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("Source object");
            var instaPosts = new InstaPostList();
            instaPosts.AddRange(
                SourceObject.Items.Select(ConvertersFabric.GetSinglePostConverter)
                    .Select(converter => converter.Convert()));
            return instaPosts;
        }
    }
}