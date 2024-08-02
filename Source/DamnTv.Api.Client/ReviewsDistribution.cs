using System.Text.Json.Serialization;

namespace DamnTv.Api.Client
{
    [JsonSerializable(typeof(ReviewsDistribution))]
    public record ReviewsDistribution
    {
        [JsonPropertyName("distribution")]
        public required IDictionary<int, int> Distribution { get; init; }
    }
}