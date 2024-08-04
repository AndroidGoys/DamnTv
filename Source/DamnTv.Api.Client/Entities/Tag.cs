using System.Text.Json.Serialization;

namespace DamnTv.Api.Client.Entities
{
    public class Tag
    {
        [JsonPropertyName("id")]
        public required int Id { get; init; }

        [JsonPropertyName("name")]
        public required string Name { get; init; }
    }
}