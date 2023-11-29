using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Position
    {
        [JsonPropertyName("id")]
        public int Id { get; } = -1;

        [JsonPropertyName("title")]
        public string Title { get; } = string.Empty;

        [JsonPropertyName("salary")]
        public decimal Salary { get; } = 0;

        [JsonConstructor]
        public Position(int id, string title, decimal salary)
        {
            Id = id;
            Title = title;
            Salary = salary;
        }

        public Position() { }
    }
}
