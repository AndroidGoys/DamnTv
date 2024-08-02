using System.ComponentModel;

using DamnTv.Frontend.Client.Features.ViewModels;
using DamnTv.Frontend.Client.Pages.Models;
using DamnTv.Frontend.Client.Widgets.Models;
using DamnTv.Frontend.Client.Widgets.ViewModels;

namespace DamnTv.Frontend.Client.Pages.ViewModels
{
    public interface ISharingViewModel : INotifyPropertyChanged
    {
        bool IsInitialized { get; }
        bool IsNotFound { get; }
        string NotFoundMessage { get; }

        MessengerMetaHeadersViewModel? MessengerMetaHeaders { get; }
        ISharingWidgetViewModel? SharingWidget { get; }
        Task<SharingPersistingState?> InitializeAsync(SharingParameters parameters);
        Task InitializeAsync(SharingParameters parameters, SharingPersistingState persistingState);
    }
}
