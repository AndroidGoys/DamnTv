using System.Collections.ObjectModel;

using DamnTv.Api.Client.Entities;
using DamnTv.Frontend.Client.Features.ViewModels;
using DamnTv.Frontend.Client.Shared.ViewModels;
using DamnTv.Frontend.Client.Widgets.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using DamnTv.Api.Client;
using DamnTv.Api.Client.Models;

namespace DamnTv.Frontend.Client.Widgets.ViewModels
{
    public class SharingWidgetViewModel : BaseViewModel, ISharingWidgetViewModel
    {
        public SharingWidgetViewModel(
            ChannelDetails channelDetails,
            TvReleases? channelReleases,
            NormalizedSharingParameters parameters
        )
        {
            _title = $"{channelDetails.Name}";
            _channelImageUrl = channelDetails.ImageUrl ?? string.Empty;
            _channelDescription = channelDetails.Description ?? string.Empty;
            OpenInAppLinkViewModel openInAppLink = new(parameters.ChannelId, parameters.TimeStart);

            _releases = new(channelReleases, openInAppLink);
            _viewLinks = new(channelDetails.ViewUrls, openInAppLink);

            _currentTab = SharingWidgetTab.TvProgram;

            SelectTvProgramTab = (args) => CurrentTab = SharingWidgetTab.TvProgram;
            SelectViewLinksTab = (args) => CurrentTab = SharingWidgetTab.ViewLinks;
        }


        private string _title;
        public string Title
        {
            get => _title;
            private set => SetProperty(ref _title, value);
        }

        private string _channelImageUrl;
        public string ChannelImageUrl
        {
            get => _channelImageUrl;
            private set => SetProperty(ref _channelImageUrl, value);
        }

        private string _channelDescription;
        public string ChannelDescription
        {
            get => _channelDescription;
            private set => SetProperty(ref _channelDescription, value);
        }

        private ReleasesListViewModel _releases;
        public ReleasesListViewModel Releases
        {
            get => _releases;
            private set => SetProperty(ref _releases, value);
        }

        private ViewLinksViewModel _viewLinks;
        public ViewLinksViewModel ViewLinks
        {
            get => _viewLinks;
            private set => SetProperty(ref _viewLinks, value);
        }

        private SharingWidgetTab _currentTab;
        public SharingWidgetTab CurrentTab
        {
            get => _currentTab;
            private set => SetProperty(ref _currentTab, value);
        }

        public Action<MouseEventArgs> SelectTvProgramTab { get; }
        public Action<MouseEventArgs> SelectViewLinksTab { get; }
    }
}
