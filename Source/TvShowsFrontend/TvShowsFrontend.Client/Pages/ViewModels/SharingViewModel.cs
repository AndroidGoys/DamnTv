using TvApi;
using TvApi.Entities;

using TvShowsFrontend.Client.Features.ViewModels;
using TvShowsFrontend.Client.Shared.ViewModels;
using TvShowsFrontend.Client.Shared.Views;
using TvShowsFrontend.Client.Widgets.Models;
using TvShowsFrontend.Client.Widgets.ViewModels;

namespace TvShowsFrontend.Client.Pages.ViewModels;

public class SharingViewModel(
    ILogger<SharingViewModel> logger,
    MinimalTvApiClient apiClient,
    IServiceProvider services
) : BaseViewModel, ISharingViewModel
{
    private readonly int _defaultShowsLimit = 4;
    private readonly IServiceProvider _services = services;
    private readonly ILogger _logger = logger;
    private readonly MinimalTvApiClient _apiClient = apiClient;

    private ISharingWidgetViewModel? _sharingWidget = null;
    public ISharingWidgetViewModel? SharingWidget {
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

    private MessengerMetaHeadersViewModel? _messengerMetaHeaders;
    public MessengerMetaHeadersViewModel? MessengerMetaHeaders {
        get => _messengerMetaHeaders; 
        set => SetProperty(ref _messengerMetaHeaders, value); 
    }

    public async Task InitializeAsync(SharingParameters parameters) 
    {
        try
        {
            int limit = (parameters.Limit.HasValue) ? parameters.Limit.Value : _defaultShowsLimit;

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
            TvReleases channelReleases = await getReleasesTask;

            ILogger<SharingWidgetViewModel> sharingLogger = _services
                .GetRequiredService<ILogger<SharingWidgetViewModel>>();

            TvChannelRelease? firstRelease = channelReleases.Releases.FirstOrDefault();

            _messengerMetaHeaders = new(
                firstRelease?.ShowName,
                channelDetails.Name,
                firstRelease?.Description,
                channelDetails.ImageUrl
            );

            _sharingWidget = new SharingWidgetViewModel(channelDetails, channelReleases, timeStart, parameters, sharingLogger);
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
