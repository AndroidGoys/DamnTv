using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Channels;

using Microsoft.Extensions.Logging;
using TvApi.Entities;
using TvApi.Exceptions;
using TvApi.Models;

namespace TvApi
{
    public class MinimalTvApiClient
    {
        private readonly string _baseRoute;
        private readonly ILogger _logger;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public MinimalTvApiClient(ILogger<MinimalTvApiClient> logger, HttpClient client)
        {
            _baseRoute = "http://176.109.106.211/api";
            _logger = logger;
            _client = client;
            _serializerOptions = new() { RespectNullableAnnotations = true };
            _client.BaseAddress = new Uri(_baseRoute);
        }

        private async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
        {
            HttpResponseMessage message = await _client.SendAsync(request);

            if (message.Content.Headers.ContentLength == 0)
            {
                RequestDetails details = new(request, message);
                throw new TvRequestException(
                    details,
                    "Request body was expected"
                );
            }

            //Stream bodyStream = await message.Content.ReadAsStreamAsync();
            string body = await message.Content.ReadAsStringAsync();
            if (message.StatusCode >= HttpStatusCode.BadRequest)
            {
                RequestDetails details = new(request, message);
                throw new TvRequestException(
                    details,
                    message.StatusCode.ToString()
                );
            }

            return JsonSerializer.Deserialize<T>(
                body,
                _serializerOptions
            ) ?? throw new NullReferenceException();
        }

        private async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string route
        ){
            string fullRoute = _baseRoute + route;
            HttpRequestMessage message = new(
                method,
                new Uri(fullRoute)
            );

            return await SendRequestAsync<TResult>(message);
        }

        private async Task<TResult> SendRequestAsync<TResult, TBody>(
            HttpMethod method,
            string route,
            TBody body
        ) where TBody : class
        {
            HttpRequestMessage message = new(
                method,
                Path.Combine(_baseRoute, route)
            );
            Stream stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, body, _serializerOptions);
            message.Content = new StreamContent(stream);

            return await SendRequestAsync<TResult>(message);
        }

        public async Task<ChannelDetails> GetChannelDetailsAsync(int channelId)
        {
            return await SendRequestAsync<ChannelDetails>(
                HttpMethod.Get,
                $"/channels/{channelId}"
            );
        }

        public async Task<TvReleases> GetChannelReleasesAsync(
            int channelId,
            int limit = -1,
            DateTimeOffset? timeStart = null
        ){
            timeStart = timeStart ?? DateTimeOffset.MinValue;
            long timeStartSeconds = timeStart.Value.ToUniversalTime().ToUnixTimeSeconds();
            float timeZone = (float)timeStart.Value.Offset.TotalHours;

            TvReleasesModel releases = await SendRequestAsync<TvReleasesModel>(
                HttpMethod.Get,
                $"/channels/{channelId}/releases?limit={limit}" +
                    $"&time-start={timeStartSeconds}" +
                    $"&time-zone={timeZone}"
            );

            return TvReleases.FromModel(releases, timeStart.Value.Offset);
        }

        public async Task<ReviewsDistribution> GetChannelReviewsDistributionAsync(
            int channelId
        ){
            return await SendRequestAsync<ReviewsDistribution>(
                HttpMethod.Get,
                $"/channels/{channelId}/reviews/distribution"
            );
        }

        public async Task<ReviewsDistribution> GetShowReviewsDistributionAsync(
            int showId
        ){
            return await SendRequestAsync<ReviewsDistribution>(
                HttpMethod.Get,
                $"/channels/{showId}/reviews/distribution"
            );
        }
    }
}
