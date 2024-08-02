using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DamnTv.Api.Client.Entities
{
    [JsonSerializable(typeof(ChannelDetails))]
    public record ChannelDetails
    {
        [JsonPropertyName("id")]
        public required int Id { get; init; }

        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("imageUrl")]
        public required string? ImageUrl { get; init; }

        [JsonPropertyName("description")]
        public required string? Description { get; init; }

        [JsonPropertyName("assessment")]
        public required float Assessment { get; init; }

        [JsonPropertyName("tags")]
        public required IEnumerable<Tag> Tags { get; init; }

        [JsonPropertyName("viewUrls")]
        public required IEnumerable<string> ViewUrls { get; init; }
    }
}
