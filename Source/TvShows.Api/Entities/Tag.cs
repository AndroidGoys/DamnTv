using System.Text.Json.Serialization;

namespace TvApi.Entities
{
    [JsonSerializable(typeof(Tag))]
    public class Tag
    {
        [JsonPropertyName("id")]
        public required int Id { get; init; }

        [JsonPropertyName("name")]
        public required string Name { get; init; }
    }
}