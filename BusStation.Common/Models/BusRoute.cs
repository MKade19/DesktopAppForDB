using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class BusRoute
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = -1;

        [JsonPropertyName("routeNumber")]
        public string RouteNumber { get; set; } = string.Empty;

        [JsonPropertyName("departure")]
        public string Departure { get; set; } = string.Empty;

        [JsonPropertyName("destination")]
        public string Destination { get; set; } = string.Empty;

        [JsonPropertyName("distance")]
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
