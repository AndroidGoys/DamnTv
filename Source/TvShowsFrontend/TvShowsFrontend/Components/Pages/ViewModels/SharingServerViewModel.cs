using TvApi;

using TvShowsFrontend.Client.Pages.ViewModels;
using TvShowsFrontend.Client.Widgets.Models;

namespace TvShowsFrontend.Components.Pages.ViewModels
{
    public class SharingServerViewModel(
        ILogger<SharingServerViewModel> logger,
        MinimalTvApiClient apiClient,
        IServiceProvider services
    ) : SharingViewModel(logger, apiClient, services), ISharingViewModel
    {
       
        public override async Task InitializeAsync(SharingParameters parameters)
        {
            parameters.Limit = 1;
            await base.InitializeAsync(parameters);
            //IsInitialized = false;
        }
    }
}
