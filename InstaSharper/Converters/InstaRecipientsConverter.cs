using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaRecipientsConverter : IObjectConverter<InstaRecipientThreads, IInstaRecipientsResponse>
    {
        public IInstaRecipientsResponse SourceObject { get; set; }

        public InstaRecipientThreads Convert()
        {
            var recipients = new InstaRecipientThreads
            {
                ExpiresIn = SourceObject.Expires,
                Filtered = SourceObject.Filtered,
                RankToken = SourceObject.RankToken,
                RequestId = SourceObject.RequestId
            };
            foreach (var recipient in SourceObject.RankedRecipients)
            {
                var rankedThread = new InstaRankedRecipientThread
                {
                    Canonical = recipient.Thread.Canonical,
                    Named = recipient.Thread.Named,
                    Pending = recipient.Thread.Pending,
                    ThreadId = recipient.Thread.ThreadId,
                    ThreadTitle = recipient.Thread.ThreadTitle,
                    ThreadType = recipient.Thread.ThreadType,
                    ViewerId = recipient.Thread.ViewerId
                };
                foreach (var user in recipient.Thread.Users)
                    rankedThread.Users.Add(ConvertersFabric.GetUserShortConverter(user).Convert());
                recipients.Items.Add(rankedThread);
            }

            return recipients;
        }
    }
}