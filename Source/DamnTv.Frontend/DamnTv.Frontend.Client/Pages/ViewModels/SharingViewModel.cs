﻿using DamnTv.Api.Client;
using DamnTv.Api.Client.Entities;
using DamnTv.Frontend.Client.Features.ViewModels;
using DamnTv.Frontend.Client.Pages.Models;
using DamnTv.Frontend.Client.Shared.ViewModels;
using DamnTv.Frontend.Client.Widgets.Models;
using DamnTv.Frontend.Client.Widgets.ViewModels;

namespace DamnTv.Frontend.Client.Pages.ViewModels;

public class SharingViewModel(
    ILogger<SharingViewModel> logger,
    MinimalTvApiClient apiClient,
    IServiceProvider services
) : BaseViewModel, ISharingViewModel
{
    protected readonly IServiceProvider Services = services;
    protected readonly ILogger Logger = logger;
    protected readonly MinimalTvApiClient ApiClient = apiClient;

    private ISharingWidgetViewModel? _sharingWidget = null;
    public ISharingWidgetViewModel? SharingWidget
    {
        get => _sharingWidget;
        protected set => SetProperty(ref _sharingWidget, in value);
    }

    public string NotFoundMessage { get; } = "Канал не найден";

    private bool _isNotFound = false;
    public bool IsNotFound
    {
        get => _isNotFound;
        protected set => SetProperty(ref _isNotFound, value);
    }

    private bool _isInitialized = false;
    public bool IsInitialized
    {
        get => _isInitialized;
        protected set => SetProperty(ref _isInitialized, value);
    }

    private MessengerMetaHeadersViewModel? _messengerMetaHeaders;
    public MessengerMetaHeadersViewModel? MessengerMetaHeaders
    {
        get => _messengerMetaHeaders;
        protected set => SetProperty(ref _messengerMetaHeaders, value);
    }

    public virtual async Task<SharingPersistingState?> InitializeAsync(SharingParameters parameters)
    {
        try
        {
            Logger.LogDebug("Новое состояние");
            NormalizedSharingParameters normalizedParameters = parameters.Normalize();

            Task<ChannelDetails> getDetailsTask = ApiClient.GetChannelDetailsAsync(parameters.ChannelId);
            Task<TvReleases> getReleasesTask = ApiClient.GetChannelReleasesAsync(
                parameters.ChannelId,
                normalizedParameters.Limit,
                normalizedParameters.TimeStart
            );

            ChannelDetails details = await getDetailsTask;
            TvReleases releases = await getReleasesTask;

            InitializeTree(parameters, details, releases);

            return new(details, releases);
        }
        catch (Exception ex)
        {
            IsNotFound = true;
            Logger.LogError(ex, "SharingWidgetInitializing error");
            return new(null, null);
        }
        finally
        {
            IsInitialized = true;
        }
    }

    public virtual Task InitializeAsync(SharingParameters parameters, SharingPersistingState persistingState)
    {
        try
        {
            Logger.LogDebug("восстановление состояния");
            ChannelDetails? channelDetails = persistingState.ChannelDetails;
            TvReleases? channelReleases = persistingState.ChannelReleases;

            InitializeTree(
                parameters,
                channelDetails,
                channelReleases
            );
            return Task.CompletedTask;
        }
        finally
        {
            IsInitialized = true;
        }
    }

    private void InitializeTree(
        SharingParameters parameters,
        ChannelDetails? channelDetails,
        TvReleases? channelReleases
    )
    {

        if (channelDetails == null)
        {
            IsNotFound = true;
            return;
        }

        TvChannelRelease? firstRelease = channelReleases?.Releases?.FirstOrDefault();
        string title = "Расписание не найдено";
        if (firstRelease != null)
        {
            title = firstRelease.ToString();
        }

        _messengerMetaHeaders = new(
            channelDetails.Name,
            title,
            firstRelease?.Description,
            CreatePreviewLink(parameters)
        );

        _sharingWidget = new SharingWidgetViewModel(
            channelDetails,
            channelReleases,
            parameters.Normalize()
        );
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
            string strParams = string.Join("&", urlParams);
            previewUrl += $"?{strParams}";
        }

        return previewUrl;
    }
}
