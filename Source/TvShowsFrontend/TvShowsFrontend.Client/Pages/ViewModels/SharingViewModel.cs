using TvApi;
using TvApi.Entities;
using TvShowsFrontend.Client.Shared.ViewModels;
using TvShowsFrontend.Client.Widgets.Models;
using TvShowsFrontend.Client.Widgets.ViewModels;

namespace TvShowsFrontend.Client.Pages.ViewModels;

public class SharingViewModel(
    ILogger<SharingViewModel> logger,
    MinimalTvApiClient apiClient,
    ISharingWidgetViewModel sharingWidget

) : BaseViewModel, ISharingViewModel
{
    private readonly ILogger _logger = logger;
    private readonly MinimalTvApiClient _apiClient = apiClient;

    private ISharingWidgetViewModel _sharingWidget = sharingWidget;
    public ISharingWidgetViewModel SharingWidget {
        get => _sharingWidget;
        set => SetProperty(ref _sharingWidget, in value);
    }

    public async Task InitializeAsync(SharingParameters parameters) 
    {
        await SharingWidget.InitializeAsync(parameters);
    }   
}
