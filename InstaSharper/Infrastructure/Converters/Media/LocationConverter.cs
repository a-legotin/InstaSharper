using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Models.Response.Location;

namespace InstaSharper.Infrastructure.Converters.Media;

internal class LocationConverter : IObjectConverter<InstaLocation, InstaLocationResponse>
{
    public InstaLocation Convert(InstaLocationResponse source)
    {
        return new InstaLocation
        {
            Address = source.Address,
            City = source.City,
            Height = source.Height,
            Lat = source.Lat,
            Lng = source.Lng,
            Name = source.Name,
            Pk = source.Pk,
            Rotation = source.Rotation,
            Width = source.Width,
            X = source.X,
            Y = source.Y,
            ExternalId = source.ExternalId,
            ExternalSource = source.ExternalIdSource,
            ShortName = source.ShortName,
            FacebookPlacesId = source.FacebookPlacesId
        };
    }
}