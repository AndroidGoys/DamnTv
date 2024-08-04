using System.Text.Json.Serialization;

using DamnTv.Api.Client.Entities;

namespace DamnTv.Api.Client.Models
{
    internal record TvChannelReleaseModel
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