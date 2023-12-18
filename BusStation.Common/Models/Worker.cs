using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Worker
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; } = -1;

        [JsonPropertyName("fullname")]
        [Required]
        [StringLength(50)]
        public string Fullname { get; set; } = string.Empty;

        [JsonPropertyName("birth_date")]
        [Required]
        public DateTime BirthDate { get; set; } = DateTime.Today;

        [JsonPropertyName("experience")]
        [Required]
        [Range(1, int.MaxValue)]
        public int Experience { get; set; } = 1;

        [JsonPropertyName("position_id")]
        [Required]
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
