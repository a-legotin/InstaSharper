using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaRecipientsConverter : IObjectConverter<InstaRecipients, InstaRecipientsResponse>
    {
        public InstaRecipientsResponse SourceObject { get; set; }

        public InstaRecipients Convert()
        {
            var recipients = new InstaRecipients();

            return recipients;
        }
    }
}