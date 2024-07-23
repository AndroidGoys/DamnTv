using System.ComponentModel;

using TvApi;
using TvApi.Entities;

using TvShowsFrontend.Client.Features.ViewModels;
using TvShowsFrontend.Client.Features.Views;
using TvShowsFrontend.Client.Pages.ViewModels;
using TvShowsFrontend.Client.Shared.ViewModels;
using TvShowsFrontend.Client.Widgets.Models;
using TvShowsFrontend.Client.Widgets.ViewModels;
using TvShowsFrontend.Client.Widgets.Views;

namespace TvShowsFrontend.Components.Pages.ViewModels
{
    public class SharingServerViewModel(
        ILogger<SharingViewModel> logger,
        MinimalTvApiClient apiClient,
        IServiceProvider services
    ) : SharingViewModel(logger, apiClient, services), ISharingViewModel
    {
       
        public override async Task InitializeAsync(SharingParameters parameters)
        {
            parameters.Limit = 1;
            await base.InitializeAsync(parameters);
        }
    }
}
