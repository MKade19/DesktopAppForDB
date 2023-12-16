using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Worker
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = -1;

        [JsonPropertyName("fullname")]
        public string Fullname { get; set; } = string.Empty;

        [JsonPropertyName("birth_date")]
        public DateTime BirthDate { get; set; } = DateTime.Today;

        [JsonPropertyName("experience")]
        public int Experience { get; set; } = 0;

        [JsonPropertyName("position_id")]
        public int PositionId { get; set; } = -1;

        [JsonPropertyName("position_title")]
        public string? PositionName { get; set; }

        [JsonConstructor]
        public Worker(int id, string fullname, DateTime birthDate, int experience, int positionId, string? positionName)
        {
            Id = id;
            Fullname = fullname;
            BirthDate = birthDate;
            Experience = experience;
            PositionId = positionId;
            PositionName = positionName;
        }

        public Worker() { }

        public override string ToString()
        {
            return $"{Fullname}";
        }
    }
}
