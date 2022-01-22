using System;
using System.Linq;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Models.Response.Media;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Infrastructure.Converters.Media;

internal class CarouselConverter : IObjectConverter<InstaCarousel, InstaCarouselResponse>
{
    private readonly IObjectConverter<InstaUserTag, InstaUserTagResponse> _userTagConverter;

    public CarouselConverter(IObjectConverter<InstaUserTag, InstaUserTagResponse> userTagConverter)
    {
        _userTagConverter = userTagConverter;
    }

    public InstaCarousel Convert(InstaCarouselResponse source)
    {
        var carousel = new InstaCarousel();
        if (source == null) throw new ArgumentNullException("Source object");
        carousel.AddRange(source.Select(ConvertItem));
        return carousel;
    }

    private InstaCarouselItem ConvertItem(InstaCarouselItemResponse item)
    {
        if (item == null) throw new ArgumentNullException("Source object");
        var carouselItem = new InstaCarouselItem
        {
            CarouselParentId = item.CarouselParentId,
            Height = item.Height,
            Width = item.Width,
            MediaType = (InstaMediaType)item.MediaType,
            InstaIdentifier = item.InstaIdentifier,
            Pk = item.Pk
        };

        if (item?.Images?.Candidates != null)
            foreach (var image in item.Images.Candidates)
                carouselItem.Images.Add(new InstaImage(image.Url, image.Width, image.Height));
        if (item?.Videos != null)
            foreach (var video in item.Videos)
                carouselItem.Videos.Add(new InstaVideo(video.Url, video.Width, video.Height, video.Type));
        if (item.UserTagList?.In != null && item.UserTagList?.In?.Count > 0)
            foreach (var tag in item.UserTagList.In)
                carouselItem.UserTags.Add(_userTagConverter.Convert(tag));
        return carouselItem;
    }
}