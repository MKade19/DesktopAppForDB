using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class AuthData
    {
        [JsonPropertyName("token")]
        [Required]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        [Required]
        public string? Role { get; set; } = string.Empty;

        [JsonConstructor]
        public AuthData(string token, string? role) 
        {
            Token = token;
            Role = role;
        }

        public AuthData() { }
    }
}
