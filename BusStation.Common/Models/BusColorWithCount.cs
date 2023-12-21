using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class BusColorWithCount
    {
        [JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;

        [JsonPropertyName("bus_count")]
        public int BusCount { get; set; } = 0;

        public BusColorWithCount(string color, int count) 
        { 
            Color = color;
            BusCount = count;
        }

        public BusColorWithCount() { }
    }
}
