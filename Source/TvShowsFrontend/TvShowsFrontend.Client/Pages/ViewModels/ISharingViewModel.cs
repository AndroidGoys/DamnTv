using System.ComponentModel;

using TvShowsFrontend.Client.Features.ViewModels;
using TvShowsFrontend.Client.Widgets.Models;
using TvShowsFrontend.Client.Widgets.ViewModels;

namespace TvShowsFrontend.Client.Pages.ViewModels
{
    public interface ISharingViewModel : INotifyPropertyChanged
    {
        bool IsInitialized { get; }
        bool IsNotFound { get; }
        string NotFoundMessage { get; }

        MessengerMetaHeadersViewModel? MessengerMetaHeaders { get; }
        ISharingWidgetViewModel? SharingWidget { get; }
        Task InitializeAsync(SharingParameters parameters);
    }
}
