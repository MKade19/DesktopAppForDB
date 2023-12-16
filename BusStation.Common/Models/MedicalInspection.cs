using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class MedicalInspection
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = -1;

        [JsonPropertyName("inspectionDate")]
        public DateTime InspectionDate { get; set; } = DateTime.Today;

        [JsonPropertyName("workerId")]
        public int WorkerId { get; set; } = -1;

        [JsonPropertyName("workerName")]
        public string? WorkerName { get; set; }

        [JsonPropertyName("isAllowed")]
        public bool IsAllowed { get; set; } = true;

        [JsonPropertyName("denialReason")]
        public string? DenialReason { get; set; } = null;

        [JsonConstructor]
        public MedicalInspection(int id, DateTime inspectionDate, int workerId, bool isAllowed, string? denialReason, string? workerName)
        {
            Id = id;
            InspectionDate = inspectionDate;
            WorkerId = workerId;
            IsAllowed = isAllowed;
            DenialReason = denialReason;
            WorkerName = workerName;
        }

        public MedicalInspection() { }
    }
}
