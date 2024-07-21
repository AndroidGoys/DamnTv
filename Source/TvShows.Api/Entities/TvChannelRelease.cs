using System.Text.Json.Serialization;

namespace TvApi.Entities
{
    [JsonSerializable(typeof(TvChannelRelease))]
    public record TvChannelRelease
    {
        [JsonPropertyName("showId")]
        public required int ShowId { get; init; }

        [JsonPropertyName("showName")]
        public required string ShowName { get; init; }

        [JsonPropertyName("showAssessment")]
        public required float ShowAssessment { get; init; }

        [JsonPropertyName("showAgeLimit")]
        public required AgeLimit ShowAgeLimit { get; init; }

        [JsonPropertyName("previewUrl")]
        public required string? PreviewUrl { get; init; }

        [JsonPropertyName("description")]
        public required string? Description { get; init; }

        [JsonPropertyName("timeStart")]
        public required long TimeStart { get; init; }

        [JsonPropertyName("timeStop")]
        public required long TimeStop { get; init; }
    }
}