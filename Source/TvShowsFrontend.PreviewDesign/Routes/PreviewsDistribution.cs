using Microsoft.AspNetCore.Mvc;

using TvApi;
using TvApi.Entities;

using SkiaSharp;
using TvShowsFrontend.PreviewDesign.Models;

namespace TvShowsFrontend.PreviewDesign.Routes
{
    public static class PreviewsDistribution
    {
        public static RouteHandlerBuilder MapPreviewsDistribution(this WebApplication app)
            => app.MapGet("/sharing/{id}/preview", GetPreview);


        private static async Task<IResult> GetPreview(
            [FromRoute] int id, 
            [FromServices] MinimalTvApiClient apiClient,
            [FromServices] IPreviewBuilder previewBuilder,
            [FromServices] HttpClient httpClient
        )
        {
            ChannelDetails channel = await apiClient.GetChannelDetailsAsync(id);
            TvReleases releases = await apiClient.GetChannelReleasesAsync(id);
            TvChannelRelease release = releases.Releases.First();


            HttpResponseMessage response = await httpClient.GetAsync(channel.ImageUrl);
            Stream channelImageStream = await response.Content.ReadAsStreamAsync();

            Stream preview = previewBuilder
               .WithChannelName(channel.Name)
               .WithChannelImage(channelImageStream)
               .WithShowName(release.ShowName)
               .WithProgress(0.5f)
               .WithShowDescription(release.Description)
               .Build();


            return Results.File(preview, "image/png");
        }
    }
}
