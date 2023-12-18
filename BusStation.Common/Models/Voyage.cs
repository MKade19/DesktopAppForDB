using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Voyage
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; } = -1;

        [JsonPropertyName("voyage_date")]
        [Required]
        public DateTime VoyageDate { get; set; } = DateTime.Today;

        [JsonPropertyName("departure_time")]
        [Required]
        public DateTime DepartureTime { get; set; } = DateTime.Today;

        [JsonPropertyName("arrival_time")]
        [Required]
        public DateTime ArrivalTime { get; set; } = DateTime.Today;

        [JsonPropertyName("route_id")]
        [Required]
        public int BusRouteId { get; set; } = -1;

        [JsonPropertyName("route_number")]
        [Required]
        public string? BusRouteNumber { get; set; }

        [JsonPropertyName("worker_id")]
        [Required]
        public int WorkerId { get; set; } = -1;

        [JsonPropertyName("worker_name")]
        public string? WorkerName { get; set; }

        [JsonPropertyName("bus_id")]
        public int BusId { get; set; } = -1;

        [JsonPropertyName("bus_number")]
        public string? BusNumber { get; set; }

        [JsonConstructor]
        public Voyage
        (
            int id, 
            DateTime voyageDate,
            DateTime departureTime,
            DateTime arrivalTime, 
            int busRouteId, 
            int workerId, 
            int busId,
            string? busRouteNumber,
            string? workerName,
            string? busNumber
        )
        {
            Id = id;
            VoyageDate = voyageDate;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            BusRouteId = busRouteId;
            WorkerId = workerId;
            BusId = busId;
            BusRouteNumber = busRouteNumber;
            BusNumber = busNumber;
            WorkerName = workerName;
        }

        public Voyage() { }
    }
}
