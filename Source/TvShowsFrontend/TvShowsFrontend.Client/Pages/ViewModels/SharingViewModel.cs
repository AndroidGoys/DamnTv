using TvApi;
using TvApi.Entities;
using TvShowsFrontend.Client.Shared.ViewModels;
using TvShowsFrontend.Client.Shared.Views;
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

    public string NotFoundMessage { get; } = "Канал не найден";

    private bool _isNotFound = false;
    public bool IsNotFound {
        get => _isNotFound;
        set => SetProperty(ref _isNotFound, value);
    }

    private bool _isInitialized = false;
    public bool IsInitialized {
        get => _isInitialized;
        private set => SetProperty(ref _isInitialized, value); 
    }

    public async Task InitializeAsync(SharingParameters parameters) 
    {
        try
        {
            await SharingWidget.InitializeAsync(parameters);
        }
        catch (Exception ex) 
        {
            IsNotFound = true;
            _logger.LogError(ex, "SharingWidgetInitializing error");
        }
        finally
        {
            IsInitialized = true;
        }
    }   
}
