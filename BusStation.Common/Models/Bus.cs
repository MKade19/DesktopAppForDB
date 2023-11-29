using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Bus
    {
        [JsonPropertyName("id")]
        public int Id { get; } = -1;

        [JsonPropertyName("stateNumber")]
        public string StateNumber { get; } = string.Empty;

        [JsonPropertyName("deliveryDate")]
        public DateTime DeliveryDate { get; } = DateTime.MinValue;

        [JsonPropertyName("color")]
        public string Color { get; } = string.Empty;

        [JsonPropertyName("garageNumber")]
        public int GarageNumber { get; } = -1;

        [JsonPropertyName("busModelId")]
        public int BusModelId { get; } = -1;

        [JsonConstructor]
        public Bus(int id, string stateNumber, DateTime deliveryDate, string color, int garageNumber, int busModelId)
        {
            Id = id;
            StateNumber = stateNumber;
            DeliveryDate = deliveryDate;
            Color = color;
            GarageNumber = garageNumber;
            BusModelId = busModelId;
        }

        public Bus() { }
    }
}
