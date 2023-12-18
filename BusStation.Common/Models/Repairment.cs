using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Repairment
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; } = -1;

        [JsonPropertyName("beginDate")]
        [Required]
        public DateTime BeginDate { get; set; } = DateTime.Today;

        [JsonPropertyName("endDate")]
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Today;

        [JsonPropertyName("workerId")]
        [Required]
        public int WorkerId { get; set; } = -1;

        [JsonPropertyName("workerName")]
        public string? WorkerName { get; set; }

        [JsonPropertyName("busId")]
        [Required]
        public int BusId { get; set; } = -1;

        [JsonPropertyName("busNumber")]
        public string? BusNumber { get; set; }

        [JsonPropertyName("malfunction")]
        [Required]
        [StringLength(100)]
        public string Malfunction { get; set; } = string.Empty;

        [JsonConstructor]
        public Repairment(int id, DateTime beginDate, DateTime endDate, int workerId, int busId, string malfunction, string? workerName, string? busNumber)
        {
            Id = id;
            BeginDate = beginDate;
            EndDate = endDate;
            WorkerId = workerId;
            BusId = busId;
            Malfunction = malfunction;
            WorkerName = workerName;
            BusNumber = busNumber;
        }

        public Repairment() { }
    }
}
