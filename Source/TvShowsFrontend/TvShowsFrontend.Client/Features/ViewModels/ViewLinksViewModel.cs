using TvShowsFrontend.Client.Shared.ViewModels;

namespace TvShowsFrontend.Client.Features.ViewModels;

public class ViewLinksViewModel(IEnumerable<string> viewLinks) : BaseViewModel
{
    public IEnumerable<Uri> ViewLinks { get; } = viewLinks
        .Select(x => new Uri(x));
}
