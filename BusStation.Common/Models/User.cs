using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = -1;

        [JsonPropertyName("username")]
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;

        public byte[]? Salt { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; } = string.Empty;

        [JsonConstructor]
        public User(int id, string username, string password, byte[]? salt, string? role) 
        { 
            Id = id;
            Username = username;
            Password = password;
            Salt = salt;
            Role = role;
        }

        public User() { }
    }
}
