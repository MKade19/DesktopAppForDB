using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class BusModel
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; } = -1;

        [JsonPropertyName("title")]
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("producer_id")]
        [Required]
        public int ProducerId { get; set; } = -1;

        [JsonPropertyName("producer_name")]
        public string? ProducerName { get; set; }

        [JsonConstructor]
        public BusModel(int id, string title, int producerId, string? producerName)
        {
            Id = id;
            Title = title;
            ProducerId = producerId;
            ProducerName = producerName;
        }

        public BusModel() { }

        public override string ToString() 
        { 
            return Title;
        }
    }
}
