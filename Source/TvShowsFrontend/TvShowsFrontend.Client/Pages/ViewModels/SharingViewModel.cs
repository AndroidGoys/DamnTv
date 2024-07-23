using System.Reflection.Metadata;

using Microsoft.AspNetCore.Components;

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
    protected readonly NavigationManager NavigationManager;
    protected readonly IServiceProvider Services = services;
    protected readonly ILogger Logger = logger;
    protected readonly MinimalTvApiClient ApiClient = apiClient;

    private ISharingWidgetViewModel? _sharingWidget = null;
    public ISharingWidgetViewModel? SharingWidget {
        get => _sharingWidget;
        protected set => SetProperty(ref _sharingWidget, in value);
    }

    public string NotFoundMessage { get; } = "Канал не найден";

    private bool _isNotFound = false;
    public bool IsNotFound {
        get => _isNotFound;
        protected set => SetProperty(ref _isNotFound, value);
    }

    private bool _isInitialized = false;
    public bool IsInitialized {
        get => _isInitialized;
        protected set => SetProperty(ref _isInitialized, value); 
    }

    private MessengerMetaHeadersViewModel? _messengerMetaHeaders;
    public MessengerMetaHeadersViewModel? MessengerMetaHeaders {
        get => _messengerMetaHeaders;
        protected set => SetProperty(ref _messengerMetaHeaders, value); 
    }

    public virtual async Task InitializeAsync(SharingParameters parameters) 
    {
        try
        {
            NormalizedSharingParameters normalizedParametrs = parameters.Normalize();

            Task<ChannelDetails> getDetailsTask = ApiClient.GetChannelDetailsAsync(parameters.ChannelId);
            Task<TvReleases> getReleasesTask = ApiClient.GetChannelReleasesAsync(
                parameters.ChannelId,
                normalizedParametrs.Limit,
                normalizedParametrs.TimeStart
            );

            ChannelDetails channelDetails = await getDetailsTask;
            TvReleases channelReleases = await getReleasesTask;


            TvChannelRelease? firstRelease = channelReleases.Releases.FirstOrDefault();

            string title = "Расписание не найдено";
            if (firstRelease != null)
            {
                DateTimeOffset timeStart = firstRelease.TimeStart;
                DateTimeOffset timeStop = firstRelease.TimeStop;
                title = $"{firstRelease.ShowName} " +
                    $"({timeStart.Hour:d2}:{timeStart.Minute:d2} - " +
                    $"{timeStop.Hour:d2}:{timeStop.Minute:d2})";
            }

            _messengerMetaHeaders = new(
                channelDetails.Name,
                title,
                firstRelease?.Description,
                CreatePreviewLink(parameters)
            );

            ILogger<SharingWidgetViewModel> sharingLogger = Services
                .GetRequiredService<ILogger<SharingWidgetViewModel>>();
            _sharingWidget = new SharingWidgetViewModel(
                channelDetails, 
                channelReleases, 
                normalizedParametrs, 
                sharingLogger
            );
        }
        catch (Exception ex) 
        {
            IsNotFound = true;
            Logger.LogError(ex, "SharingWidgetInitializing error");
        }
        finally
        {
            IsInitialized = true;
        }
    }

    private static string CreatePreviewLink(SharingParameters parameters) 
    {

        string previewUrl = $"/sharing/{parameters.ChannelId}/preview";
        List<string> urlParams = new(2);
        if (parameters.TimeStart.HasValue)
            urlParams.Add($"time-start={parameters.TimeStart}");

        if (parameters.TimeZone.HasValue)
            urlParams.Add($"time-zone={parameters.TimeZone}");

        if (urlParams.Count > 0)
        {
            string strParams = String.Join("&", urlParams);
            previewUrl += $"?{strParams}";
        }

        return previewUrl;
    }
}
