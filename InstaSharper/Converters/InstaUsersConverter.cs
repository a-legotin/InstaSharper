using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaUsersConverter : IObjectConverter<InstaUser, InstaUserResponse>
    {
        public InstaUserResponse SourceObject { get; set; }

        public InstaUser Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var shortConverter = ConvertersFabric.GetUserShortConverter(SourceObject);
            var user = new InstaUser(shortConverter.Convert())
            {
                HasAnonymousProfilePicture = SourceObject.HasAnonymousProfilePicture,
                Biography = SourceObject.Biography,
                Birthday = SourceObject.Birthday,
                CountryCode = SourceObject.CountryCode,
                NationalNumber = SourceObject.NationalNumber,
                Email = SourceObject.Email,
                ExternalUrl = SourceObject.ExternalURL,
                ShowConversionEditEntry = SourceObject.ShowConversationEditEntry,
                Gender = SourceObject.Gender,
                PhoneNumber = SourceObject.PhoneNumber
            };

            if (SourceObject.HDProfilePicVersions?.Length > 0)
                foreach (var imageResponse in SourceObject.HDProfilePicVersions)
                {
                    var converter = ConvertersFabric.GetImageConverter(imageResponse);
                    user.HdProfileImages.Add(converter.Convert());
                }

            if (SourceObject.HDProfilePicture != null)
            {
                var converter = ConvertersFabric.GetImageConverter(SourceObject.HDProfilePicture);
                user.HdProfilePicture = converter.Convert();
            }

            return user;
        }
    }
}