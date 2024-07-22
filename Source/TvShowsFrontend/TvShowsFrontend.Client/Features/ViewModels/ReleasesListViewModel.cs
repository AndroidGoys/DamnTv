using System.Collections.ObjectModel;

using TvApi.Entities;

using TvShowsFrontend.Client.Shared.ViewModels;

namespace TvShowsFrontend.Client.Features.ViewModels;

public class ReleasesListViewModel(TvReleases releases) : BaseViewModel
{
    private bool _isExpanded = false;
    public bool IsExpanded { 
        get => _isExpanded;
        private set => SetProperty(ref _isExpanded, value); 
    }

    public ObservableCollection<ReleaseViewModel> Releases { get; } =
        new(
            releases.Releases
            .Select(release => new ReleaseViewModel(release))
            .ToList()
        );
}
