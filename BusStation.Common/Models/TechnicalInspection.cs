using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class TechnicalInspection
    {
        [JsonPropertyName("id")]
        public int Id { get; } = -1;

        [JsonPropertyName("inspectionDate")]
        public DateTime InspectionDate { get; } = DateTime.MinValue;

        [JsonPropertyName("busId")]
        public int BusId { get; } = -1;

        [JsonPropertyName("isAllowed")]
        public bool IsAllowed { get; } = false;

        [JsonPropertyName("denialReason")]
        public string? DenialReason { get; }

        [JsonConstructor]
        public TechnicalInspection(int id, DateTime inspectionDate, int busId, bool isAllowed, string? denialReason)
        {
            Id = id;
            InspectionDate = inspectionDate;
            BusId = busId;
            IsAllowed = isAllowed;
            DenialReason = denialReason;
        }

        public TechnicalInspection() { }
    }
}
