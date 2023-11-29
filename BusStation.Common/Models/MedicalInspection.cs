using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class MedicalInspection
    {
        [JsonPropertyName("id")]
        public int Id { get; } = -1;

        [JsonPropertyName("inspectionDate")]
        public DateTime InspectionDate { get; } = DateTime.MinValue;

        [JsonPropertyName("workerId")]
        public int WorkerId { get; } = -1;

        [JsonPropertyName("isAllowed")]
        public bool IsAllowed { get; } = false;

        [JsonPropertyName("denialReason")]
        public string? DenialReason { get; } = null;

        [JsonConstructor]
        public MedicalInspection(int id, DateTime inspectionDate, int workerId, bool isAllowed, string? denialReason)
        {
            Id = id;
            InspectionDate = inspectionDate;
            WorkerId = workerId;
            IsAllowed = isAllowed;
            DenialReason = denialReason;
        }

        public MedicalInspection() { }
    }
}
