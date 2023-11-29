using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class BusProducer
    {
        [JsonPropertyName ("id")]
        public int Id { get; } = -1;

        [JsonPropertyName ("title")]
        public string Title { get; } = string.Empty;

        [JsonPropertyName("town")]
        public string Town { get; } = string.Empty;

        [JsonConstructor]
        public BusProducer(int id, string title, string town)
        {
            Id = id;
            Title = title;
            Town = town;
        }

        public BusProducer() { }
    }
}
