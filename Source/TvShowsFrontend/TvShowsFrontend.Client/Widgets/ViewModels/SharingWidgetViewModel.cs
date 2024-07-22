using System.Collections.ObjectModel;

using TvApi;
using TvApi.Entities;
using TvApi.Models;
using TvShowsFrontend.Client.Features.ViewModels;
using TvShowsFrontend.Client.Shared.ViewModels;
using TvShowsFrontend.Client.Widgets.Models;
using TvShowsFrontend.Client.Widgets.Views;

namespace TvShowsFrontend.Client.Widgets.ViewModels
{
    public class SharingWidgetViewModel(
        MinimalTvApiClient apiClient,
        ILogger<SharingWidgetViewModel> logger
    ) : BaseViewModel, ISharingWidgetViewModel
    {
        private readonly MinimalTvApiClient _apiClient = apiClient;
        private readonly ILogger _logger = logger;

        private ChannelDetails? _channelDetails = null;
        private TvReleases? _channelReleases = null;

        private string _title = "Давай посмотрим...";
        public string Title {
            get => _title;
            private set => SetProperty(ref _title, value);
        }

        private string _channelImageUrl = String.Empty;
        public string ChannelImageUrl {
            get => _channelImageUrl;
            private set => SetProperty(ref _channelImageUrl, value);
        }

        private string _channelDescription = String.Empty;
        public string ChannelDescription {
            get => _channelDescription;
            private set => SetProperty(ref _channelDescription, value);
        }

        public string OpenInAppUrl { get; } = "http://176.109.106.211:8080/openapi";

        private ReleasesListViewModel? _releases = null;
        public ReleasesListViewModel? Releases {
            get => _releases;
            private set => SetProperty(ref _releases, value);
        }

        public async Task InitializeAsync(SharingParameters parameters)
        {
            int limit = (parameters.Limit.HasValue)? parameters.Limit.Value : 1;

            DateTimeOffset timeStart = DateTimeOffset.FromUnixTimeSeconds(
                parameters.TimeStart.GetValueOrDefault()
            );
            
            timeStart = new(
                timeStart.DateTime, 
                TimeSpan.FromHours(
                    parameters.TimeZone.GetValueOrDefault()
                )
            );

            Task<ChannelDetails> getDetailsTask = _apiClient.GetChannelDetailsAsync(parameters.ChannelId);
            Task<TvReleases> getReleasesTask = _apiClient.GetChannelReleasesAsync(
                parameters.ChannelId, 
                limit, 
                timeStart
            );

            ChannelDetails channelDetails = await getDetailsTask;
            Title = $"{channelDetails.Name}";
            ChannelImageUrl = channelDetails.ImageUrl ?? String.Empty;
            ChannelDescription = channelDetails.Description ?? String.Empty;

            TvReleases channelReleases = await getReleasesTask;
            Releases = new(channelReleases);
        }
    }
}
