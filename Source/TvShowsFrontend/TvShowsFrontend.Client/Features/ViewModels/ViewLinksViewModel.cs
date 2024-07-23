using TvShowsFrontend.Client.Shared.ViewModels;

namespace TvShowsFrontend.Client.Features.ViewModels;

public class ViewLinksViewModel(
    IEnumerable<string> viewLinks,
    OpenInAppLinkViewModel openInAppLink
) : BaseViewModel
{
    public OpenInAppLinkViewModel OpenInAppLink { get; } = openInAppLink;
    public IEnumerable<Uri> ViewLinks { get; } = viewLinks
        .Select(x => new Uri(x));
}
