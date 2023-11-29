using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class BusRoute
    {
        [JsonPropertyName("id")]
        public int Id { get; } = -1;

        [JsonPropertyName("routeNumber")]
        public string RouteNumber { get; } = string.Empty;

        [JsonPropertyName("departure")]
        public string Departure { get; } = string.Empty;

        [JsonPropertyName("destination")]
        public string Destination { get; } = string.Empty;

        [JsonPropertyName("distance")]
        public int Distance { get; } = 0;

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
    }
}
