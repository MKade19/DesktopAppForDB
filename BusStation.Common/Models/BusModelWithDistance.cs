using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class BusModelWithDistance : BusModel
    {
        [JsonPropertyName("total_distance")]
        public int TotalDistance { get; set; } = 0;
        public BusModelWithDistance(int id, string title, int producerId, string producerTitle, int totalDistance) 
            : base(id, title, producerId, producerTitle)
        { 
            TotalDistance = totalDistance;
        }

        public BusModelWithDistance() { }
    }
}
