using InstaSharper.Classes.Models;
using InstaSharper.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaFriendshipStatusConverter :
        IObjectConverter<InstaFriendshipStatus, InstaFriendshipStatusResponse>
    {
        public InstaFriendshipStatusResponse SourceObject { get; set; }

        public InstaFriendshipStatus Convert()
        {
            var friendShip = new InstaFriendshipStatus();
            friendShip.Foolowing = SourceObject.Foolowing;
            friendShip.IncomingRequest = SourceObject.IncomingRequest;
            friendShip.OutgoingRequest = SourceObject.OutgoingRequest;
            friendShip.IncomingRequest = SourceObject.IncomingRequest;
            friendShip.IsPrivate = SourceObject.IsPrivate;
            return friendShip;
        }
    }
}