using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Bus
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = -1;

        [JsonPropertyName("stateNumber")]
        public string StateNumber { get; set; } = string.Empty;

        [JsonPropertyName("deliveryDate")]
        public DateTime DeliveryDate { get; set; } = DateTime.Today;

        [JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;

        [JsonPropertyName("garageNumber")]
        public int GarageNumber { get; set; } = 0;

        [JsonPropertyName("busModelId")]
        public int BusModelId { get; set; } = -1;

        [JsonPropertyName("busModelTitle")]
        public string? BusModelTitle { get; set; }

        [JsonConstructor]
        public Bus(int id, string stateNumber, DateTime deliveryDate, string color, int garageNumber, int busModelId, string? busModelTitle)
        {
            Id = id;
            StateNumber = stateNumber;
            DeliveryDate = deliveryDate;
            Color = color;
            GarageNumber = garageNumber;
            BusModelId = busModelId;
            BusModelTitle=busModelTitle;
        }

        public Bus() { }

        public override string ToString()
        {
            return $"{StateNumber}";
        }
    }
}
