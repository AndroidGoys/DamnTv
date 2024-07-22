using System.Text.Json.Serialization;

namespace TvApi
{
    [JsonSerializable(typeof(ReviewsDistribution))]
    public record ReviewsDistribution {
        [JsonPropertyName("distribution")]
        public required IDictionary<int, int> Distribution { get; init; }
    }
}