using System.Collections.ObjectModel;
using TvShowsFrontend.Client.Features.ViewModels;
using TvShowsFrontend.Client.Widgets.Models;

namespace TvShowsFrontend.Client.Widgets.ViewModels
{
    public interface ISharingWidgetViewModel
    {
        string? ChannelImageUrl { get; }
        string Title { get; }
        string OpenInAppUrl { get; }
        string ChannelDescription { get; }

        ReleasesListViewModel? Releases { get; }

        Task InitializeAsync(SharingParameters parameters);
    }
}