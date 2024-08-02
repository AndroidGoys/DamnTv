using System.Collections.ObjectModel;

using DamnTv.Frontend.Client.Features.ViewModels;
using DamnTv.Frontend.Client.Widgets.Models;

using Microsoft.AspNetCore.Components.Web;

namespace DamnTv.Frontend.Client.Widgets.ViewModels
{
    public interface ISharingWidgetViewModel
    {
        string? ChannelImageUrl { get; }
        string Title { get; }
        string ChannelDescription { get; }
        SharingWidgetTab CurrentTab { get; }
        ReleasesListViewModel Releases { get; }
        ViewLinksViewModel ViewLinks { get; }

        Action<MouseEventArgs> SelectTvProgramTab { get; }
        Action<MouseEventArgs> SelectViewLinksTab { get; }
    }
}