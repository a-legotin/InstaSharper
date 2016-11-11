using System;
using InstagramAPI.Classes.Models;
using InstagramAPI.ResponseWrappers;

namespace InstagramAPI.Converters
{
    internal class InstaUsersConverter : IObjectConverter<InstaUser, InstaUserResponse>
    {
        public InstaUserResponse SourceObject { get; set; }

        public InstaUser Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("Source object");
            var user = new InstaUser
            {
                InstaIdentifier = SourceObject.Id
            };
            if (!string.IsNullOrEmpty(SourceObject.FullName)) user.FullName = SourceObject.FullName;
            if (!string.IsNullOrEmpty(SourceObject.ProfilePicture)) user.ProfilePicture = SourceObject.ProfilePicture;
            if (!string.IsNullOrEmpty(SourceObject.UserName)) user.UserName = SourceObject.UserName;
            if (!string.IsNullOrEmpty(SourceObject.ExternalUrl)) user.ExternalUrl = SourceObject.ExternalUrl;
            if (!string.IsNullOrEmpty(SourceObject.IsVerified)) user.IsVerified = SourceObject.IsVerified;
            if (!string.IsNullOrEmpty(SourceObject.Pk)) user.Pk = SourceObject.Pk;
            if (SourceObject.FollowedBy != null) user.FollowedByCount = SourceObject.FollowedBy.Count;

            return user;
        }
    }
}