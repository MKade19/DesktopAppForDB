using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Repairment
    {
        [JsonPropertyName("id")]
        public int Id { get; } = -1;

        [JsonPropertyName("beginDate")]
        public DateTime BeginDate { get; } = DateTime.MinValue;

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; } = DateTime.MinValue;

        [JsonPropertyName("workerId")]
        public int WorkerId { get; } = -1;

        [JsonPropertyName("busId")]
        public int BusId { get; } = -1;

        [JsonPropertyName("malfunction")]
        public string Malfunction { get; } = string.Empty;

        [JsonConstructor]
        public Repairment(int id, DateTime beginDate, DateTime endDate, int workerId, int busId, string malfunction)
        {
            Id = id;
            BeginDate = beginDate;
            EndDate = endDate;
            WorkerId = workerId;
            BusId = busId;
            Malfunction = malfunction;
        }

        public Repairment() { }
    }
}
