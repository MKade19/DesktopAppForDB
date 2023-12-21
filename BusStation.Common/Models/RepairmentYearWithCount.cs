using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class RepairmentYearWithCount
    {
        [JsonPropertyName("repairments_year")]
        public int Year { get; set; } = 0;

        [JsonPropertyName("repairments_count")]
        public int RepairmentsCount { get; set; } = 0;

        public RepairmentYearWithCount(int year, int repairmentsCount)
        {
            Year = year;
            RepairmentsCount = repairmentsCount;
        }

        public RepairmentYearWithCount() { }
    }
}
