using System.Collections.ObjectModel;

using DamnTv.Api.Client.Entities;
using DamnTv.Frontend.Client.Shared.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DamnTv.Frontend.Client.Features.ViewModels;

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
            ?.ToList() ?? []
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
