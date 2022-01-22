namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryCTA
{
    public int LinkType { get; set; }

    public string WebUri { get; set; }

    public string AndroidClass { get; set; }

    public string Package { get; set; }

    public string DeeplinkUri { get; set; }

    public string CallToActionTitle { get; set; }

    public object RedirectUri { get; set; }

    public string LeadGenFormId { get; set; }

    public string IgUserId { get; set; }
}