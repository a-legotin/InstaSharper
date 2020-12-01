using System;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Infrastructure.Converters.User
{
    internal class
        InstaFriendshipShortStatusConverter : IObjectConverter<InstaFriendshipShortStatus,
            InstaFriendshipShortStatusResponse>
    {
        public InstaFriendshipShortStatus Convert(InstaFriendshipShortStatusResponse source)
        {
            if (source == null)
                throw new ArgumentNullException(@"InstaFriendshipShortStatusResponse is null");
            return new InstaFriendshipShortStatus
            {
                Following = source.Following,
                IncomingRequest = source.IncomingRequest,
                IsBestie = source.IsBestie,
                IsPrivate = source.IsPrivate,
                OutgoingRequest = source.OutgoingRequest
            };
        }
    }
}