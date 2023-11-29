using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Voyage
    {
        [JsonPropertyName("id")]
        public int Id { get; } = -1;

        [JsonPropertyName("voyageDate")]
        public DateTime VoyageDate { get; } = DateTime.MinValue;

        [JsonPropertyName("departureTime")]
        public TimeSpan DepartureTime { get; } = TimeSpan.Zero;

        [JsonPropertyName("arrivalTime")]
        public TimeSpan ArrivalTime { get; } = TimeSpan.Zero;

        [JsonPropertyName("routeId")]
        public int BusRouteId { get; } = -1;

        [JsonPropertyName("workerId")]
        public int WorkerId { get; } = -1;

        [JsonPropertyName("busId")]
        public int BusId { get; } = -1;

        [JsonConstructor]
        public Voyage(int id, DateTime voyageDate, TimeSpan departureTime, TimeSpan arrivalTime, int busRouteId, int workerId, int busId)
        {
            Id = id;
            VoyageDate = voyageDate;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            BusRouteId = busRouteId;
            WorkerId = workerId;
            BusId = busId;
        }

        public Voyage() { }
    }
}
