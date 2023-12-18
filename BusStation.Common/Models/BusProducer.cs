using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class BusProducer
    {
        [JsonPropertyName ("id")]
        [Required]
        public int Id { get; set; } = -1;

        [JsonPropertyName ("title")]
        [Required]
        [StringLength (30)]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("town")]
        [Required]
        [StringLength(20)]
        public string Town { get; set; } = string.Empty;

        [JsonConstructor]
        public BusProducer(int id, string title, string town)
        {
            Id = id;
            Title = title;
            Town = town;
        }

        public BusProducer() { }

        public override string ToString()
        {
            return $"{Title} ({Town})";
        }
    }
}
