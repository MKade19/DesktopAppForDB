using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class BusModel
    {
        [JsonPropertyName("id")]
        public int Id { get; } = -1;

        [JsonPropertyName("title")]
        public string Title { get; } = string.Empty;

        [JsonPropertyName("producer_id")]
        public int ProducerId { get; } = -1;

        [JsonConstructor]
        public BusModel(int id, string title, int producerId)
        {
            Id = id;
            Title = title;
            ProducerId = producerId;
        }

        public BusModel() { }
    }
}
