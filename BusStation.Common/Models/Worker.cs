using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Worker
    {
        [JsonPropertyName("id")]
        public int Id { get; } = -1;

        [JsonPropertyName("fullname")]
        public string Fullname { get; } = string.Empty;

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; } = DateTime.MinValue;

        [JsonPropertyName("experience")]
        public int Experience { get; } = 0;

        [JsonPropertyName("positionId")]
        public int PositionId { get; } = -1;

        [JsonConstructor]
        public Worker(int id, string fullname, DateTime birthDate, int experience, int positionId)
        {
            Id = id;
            Fullname = fullname;
            BirthDate = birthDate;
            Experience = experience;
            PositionId = positionId;
        }

        public Worker() { }
    }
}
