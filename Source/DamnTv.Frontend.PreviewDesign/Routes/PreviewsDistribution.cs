using Microsoft.AspNetCore.Mvc;

using SkiaSharp;
using DamnTv.Api.Client.Entities;
using DamnTv.Api.Client;
using DamnTv.Frontend.PreviewDesign.Models;

namespace DamnTv.Frontend.PreviewDesign.Routes
{
    public static class PreviewsDistribution
    {
        public static RouteHandlerBuilder MapPreviewsDistribution(this WebApplication app)
            => app.MapGet("/sharing/{id}/preview", GetPreview);


        private static async Task<IResult> GetPreview(
            [FromServices] MinimalTvApiClient apiClient,
            [FromServices] IPreviewBuilder previewBuilder,
            [FromServices] HttpClient httpClient,
            [FromRoute] int id,
            [FromQuery] float scale = 1,
            [FromQuery(Name = "time-start")] long timeStart = 0,
            [FromQuery(Name = "time-zone")] float timeZone = 0
        )
        {
            DateTimeOffset targetTime = DateTimeOffset.FromUnixTimeSeconds(timeStart);
            TimeSpan timeZoneOffset = TimeSpan.FromHours(timeZone);
            targetTime = new DateTimeOffset(
                targetTime.DateTime + timeZoneOffset,
                timeZoneOffset
            );

            ChannelDetails channel = await apiClient.GetChannelDetailsAsync(id);
            TvReleases releases = await apiClient.GetChannelReleasesAsync(id, limit: 1, timeStart: targetTime);

            IPreviewBuilder preview = previewBuilder
               .WithScaling(scale)
               .WithChannelName(channel.Name);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(channel.ImageUrl);
                Stream channelImageStream = await response.Content.ReadAsStreamAsync();

                if (response.StatusCode < System.Net.HttpStatusCode.BadRequest)
                    preview.WithChannelImage(channelImageStream);
            }
            catch { }

            if (releases.Releases.Any())
            {
                TvChannelRelease release = releases.Releases.First();



                TimeSpan currentTime = DateTimeOffset.Now - release.TimeStart;
                TimeSpan duration = release.TimeStop - release.TimeStart;

                double progress = currentTime / duration;

                preview
                   .WithShowName(release.ToString().ToUpperInvariant())
                   .WithProgress((float)progress)
                   .WithShowAssessment(release.ShowAssessment);

                if (release.Description != null)
                    preview.WithShowDescription(release.Description);
            }


            return Results.File(preview.Build(), "image/png");
        }
    }
}
