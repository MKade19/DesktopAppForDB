using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Position
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = -1;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("salary")]
        public decimal Salary { get; set; } = 0;

        [JsonConstructor]
        public Position(int id, string title, decimal salary)
        {
            Id = id;
            Title = title;
            Salary = salary;
        }

        public Position() { }

        public override string ToString()
        {
            return Title;
        }
    }
}
