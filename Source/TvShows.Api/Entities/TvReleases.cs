using System.Text.Json.Serialization;

namespace TvApi.Entities
{
    [JsonSerializable(typeof(TvReleases))]
    public record TvReleases
    {
        [JsonPropertyName("timeStart")]
        public required long TimeStart { get; init; }

        [JsonPropertyName("timeStop")]
        public required long TimeStop { get; init; }

        [JsonPropertyName("totalCount")]
        public required int TotalCount { get; init; }
        
        [JsonPropertyName("releases")]
        public required IEnumerable<TvChannelRelease> Releases { get; init; }
    }
}