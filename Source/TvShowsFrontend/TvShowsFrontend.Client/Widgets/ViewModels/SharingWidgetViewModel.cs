using System.Collections.ObjectModel;

using TvApi;
using TvApi.Entities;
using TvApi.Models;
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

        public float? ChannelAssessment => _channelDetails?.Assessment;
        public string? ChannelImageUrl => _channelDetails?.ImageUrl;
        public string ChannelDescription => _channelDetails?.Description ?? String.Empty;
        public IEnumerable<string> ChannelViewUrls => _channelDetails?.ViewUrls ?? Array.Empty<string>();
        public string OpenInAppUrl { get; } = "http://176.109.106.211:8080/openapi";

        public ObservableCollection<ReleaseWidgetViewModel> Releases { get; private set; } = new();


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

            _channelDetails = await getDetailsTask;
            Title = $"{_channelDetails.Name}";

            _channelReleases = await getReleasesTask;
            Releases = new(_channelReleases.Releases.Select(x => new ReleaseWidgetViewModel(x)));
        }
    }
}
