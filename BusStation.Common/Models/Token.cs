using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class Token
    {
        [JsonPropertyName("value")]
        [Required]
        public string Value { get; set; } = string.Empty;

        [JsonConstructor]
        public Token(string value) 
        {
            Value = value;
        }

        public Token() { }
    }
}
