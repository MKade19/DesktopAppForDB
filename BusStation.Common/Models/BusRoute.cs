using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class BusRoute
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; } = -1;

        [JsonPropertyName("routeNumber")]
        [Required]
        [StringLength(5)]
        public string RouteNumber { get; set; } = string.Empty;

        [JsonPropertyName("departure")]
        [Required]
        [StringLength(30)]
        public string Departure { get; set; } = string.Empty;

        [JsonPropertyName("destination")]
        [Required]
        [StringLength(30)]
        public string Destination { get; set; } = string.Empty;

        [JsonPropertyName("distance")]
        [Required]
        [Range(1, int.MaxValue)]
        public int Distance { get; set; } = 0;

        [JsonConstructor]
        public BusRoute(int id, string routeNumber, string departure, string destination, int distance)
        {
            Id = id;
            RouteNumber = routeNumber;
            Departure = departure;
            Destination = destination;
            Distance = distance;
        }

        public BusRoute() { }

        public override string ToString()
        {
            return $"{RouteNumber}";
        }
    }
}
