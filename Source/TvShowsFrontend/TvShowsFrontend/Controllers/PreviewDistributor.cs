using System.Runtime.CompilerServices;

using TvApi;
using TvApi.Entities;

namespace TvShowsFrontend.Controllers
{
    public static class PreviewDistributor
    {
        public static WebApplication MapPreviewDistributor(this WebApplication app)
        {
            app.MapGet("/sharing/{id}/preview", GetPreview);

            return app; 
        }

        private static async Task<IResult> GetPreview(int id, MinimalTvApiClient apiClient)
        {
            ChannelDetails channel = await apiClient.GetChannelDetailsAsync(id);
            HttpClient httpClient = new HttpClient();   
            
            HttpResponseMessage response = await httpClient.GetAsync(channel.ImageUrl);
            Stream imageStream = await response.Content.ReadAsStreamAsync();

            return Results.File(imageStream, "image/png");
        }
    }
}
