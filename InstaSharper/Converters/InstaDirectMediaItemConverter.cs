using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaDirectMediaItemConverter : IObjectConverter<InstaDirectMedia, InstaDirectMediaItemResponse>
    {
        public InstaDirectMediaItemResponse SourceObject { get; set; }

        public InstaDirectMedia Convert()
        {
            var item = new InstaDirectMedia
            {
                Text = SourceObject.Text
            };

            var converter = ConvertersFabric.Instance.GetSingleMediaConverter(SourceObject.Media);
            item.Media = converter.Convert();
            return item;
        }
    }
}