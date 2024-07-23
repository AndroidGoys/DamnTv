using System.Collections.ObjectModel;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using TvApi;
using TvApi.Entities;
using TvApi.Models;
using TvShowsFrontend.Client.Features.ViewModels;
using TvShowsFrontend.Client.Shared.ViewModels;
using TvShowsFrontend.Client.Widgets.Models;
using TvShowsFrontend.Client.Widgets.Views;

namespace TvShowsFrontend.Client.Widgets.ViewModels
{
    public class SharingWidgetViewModel: BaseViewModel, ISharingWidgetViewModel
    {
        private readonly ILogger _logger;
        public SharingWidgetViewModel(
            ChannelDetails channelDetails, 
            TvReleases channelReleases,
            NormalizedSharingParameters parameters,
            ILogger<SharingWidgetViewModel> logger
        )
        {
            _logger = logger;
            _title = String.Empty;
            _channelImageUrl = String.Empty;
            _channelDescription = String.Empty;

            _title = $"{channelDetails.Name}";
            _channelImageUrl = channelDetails.ImageUrl ?? String.Empty;
            _channelDescription = channelDetails.Description ?? String.Empty;
            OpenInAppLinkViewModel openInAppLink = new(parameters.ChannelId, parameters.TimeStart);

            _releases = new(channelReleases, openInAppLink);
            _viewLinks = new(channelDetails.ViewUrls, openInAppLink);

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

        private ReleasesListViewModel _releases;
        public ReleasesListViewModel Releases {
            get => _releases;
            private set => SetProperty(ref _releases, value);
        }

        private ViewLinksViewModel _viewLinks;
        public ViewLinksViewModel ViewLinks {
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
    }
}
