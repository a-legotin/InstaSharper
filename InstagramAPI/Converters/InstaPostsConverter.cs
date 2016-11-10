using System;
using System.Linq;
using InstagramAPI.Classes;
using InstagramAPI.ResponseWrappers;

namespace InstagramAPI.Converters
{
    internal class InstaPostsConverter : IObjectConverter<InstaPostList, InstaResponsePostList>
    {
        public InstaResponsePostList SourceObject { get; set; }

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