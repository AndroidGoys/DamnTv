using System.Collections.ObjectModel;

using Microsoft.AspNetCore.Components.Web;

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
        SharingWidgetTab CurrentTab { get; }
        ReleasesListViewModel? Releases { get; }
        ViewLinksViewModel? ViewLinks { get; }

        Action<MouseEventArgs> SelectTvProgramTab { get; }
        Action<MouseEventArgs> SelectViewLinksTab { get; }
        Task InitializeAsync(SharingParameters parameters);
    }
}