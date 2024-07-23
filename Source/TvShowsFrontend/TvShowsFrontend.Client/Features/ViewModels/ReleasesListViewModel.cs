using System.Collections.ObjectModel;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using TvApi.Entities;
using TvShowsFrontend.Client.Shared.ViewModels;

namespace TvShowsFrontend.Client.Features.ViewModels;

public class ReleasesListViewModel : BaseViewModel
{
    private bool _isCollapsed;

    public ReleasesListViewModel(TvReleases? releases, OpenInAppLinkViewModel openInApp)
    {
        _isCollapsed = true;
        ChangeVisibility = (args) => IsCollapsed = !IsCollapsed;
        NotFoundMessage = "Телепередачи за эту дату не найдены";
        OpenInAppLink = openInApp;
        Releases = new(
            releases?.Releases
            ?.Select(release => new ReleaseViewModel(release))
            ?.ToList() ?? new()
        );
    }

    public OpenInAppLinkViewModel OpenInAppLink { get; }
    public ObservableCollection<ReleaseViewModel> Releases { get; }
    public string NotFoundMessage { get; }
    public Action<MouseEventArgs> ChangeVisibility { get; }

    public bool IsCollapsed
    {
        get => _isCollapsed;
        private set => SetProperty(ref _isCollapsed, value);
    }

}
