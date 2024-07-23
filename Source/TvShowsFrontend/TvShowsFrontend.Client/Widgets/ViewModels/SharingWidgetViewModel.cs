using System.Collections.ObjectModel;

using Microsoft.AspNetCore.Components.Web;

using TvApi;
using TvApi.Entities;
using TvApi.Models;
using TvShowsFrontend.Client.Features.ViewModels;
using TvShowsFrontend.Client.Features.Views;
using TvShowsFrontend.Client.Shared.ViewModels;
using TvShowsFrontend.Client.Widgets.Models;
using TvShowsFrontend.Client.Widgets.Views;

namespace TvShowsFrontend.Client.Widgets.ViewModels
{
    public class SharingWidgetViewModel: BaseViewModel, ISharingWidgetViewModel
    {
        private readonly MinimalTvApiClient _apiClient;
        private readonly ILogger _logger;

        private int _defaultLimit;

        public SharingWidgetViewModel(MinimalTvApiClient apiClient, ILogger<SharingWidgetViewModel> logger)
        {
            _defaultLimit = 4;

            _apiClient = apiClient;
            _logger = logger;

            _title = String.Empty;
            _channelImageUrl = String.Empty;
            _channelDescription = String.Empty;

            OpenInAppUrl = "http://176.109.106.211:8080/openapi";

            _releases = null;
            _viewLinks = null;

            _currentTab = SharingWidgetTab.TvProgram;

            SelectTvProgramTab = (args) => CurrentTab = SharingWidgetTab.TvProgram;
            SelectViewLinksTab = (args) => CurrentTab = SharingWidgetTab.ViewLinks;
        }


        private string _title;
        public string Title {
            get => _title;
            private set => SetProperty(ref _title, value);
        }

        private string _channelImageUrl;
        public string ChannelImageUrl {
            get => _channelImageUrl;
            private set => SetProperty(ref _channelImageUrl, value);
        }

        private string _channelDescription;
        public string ChannelDescription {
            get => _channelDescription;
            private set => SetProperty(ref _channelDescription, value);
        }

        public string OpenInAppUrl { get; }

        private ReleasesListViewModel? _releases;
        public ReleasesListViewModel? Releases {
            get => _releases;
            private set => SetProperty(ref _releases, value);
        }

        private ViewLinksViewModel? _viewLinks;
        public ViewLinksViewModel? ViewLinks {
            get => _viewLinks;
            private set => SetProperty(ref _viewLinks, value);
        }

        private SharingWidgetTab _currentTab;
        public SharingWidgetTab CurrentTab {
            get => _currentTab;
            private set => SetProperty(ref _currentTab, value);
        }

        public Action<MouseEventArgs> SelectTvProgramTab { get; }
        public Action<MouseEventArgs> SelectViewLinksTab { get; }

        public async Task InitializeAsync(SharingParameters parameters)
        {
            int limit = (parameters.Limit.HasValue)? parameters.Limit.Value : _defaultLimit;

            TimeSpan timeZoneOffset;
            if (parameters.TimeZone.HasValue)
                timeZoneOffset = TimeSpan.FromHours(parameters.TimeZone.Value);
            else
                timeZoneOffset = TimeSpan.Zero;

            DateTimeOffset timeStart = DateTimeOffset.FromUnixTimeSeconds(
                parameters.TimeStart.GetValueOrDefault()
            );

            timeStart = new(
                timeStart.DateTime + timeZoneOffset, 
                timeZoneOffset
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
            OpenInAppLinkViewModel openInAppLink = new(parameters.ChannelId, timeStart);

            Releases = new(channelReleases, openInAppLink);
            ViewLinks = new(channelDetails.ViewUrls, openInAppLink); 
        }
    }
}
