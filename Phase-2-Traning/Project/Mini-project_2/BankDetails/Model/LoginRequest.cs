using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BankDetails.Models
{
    public class LoginRequest
    {
        [JsonProperty("Username")]
        public string? Username { get; set; }
        [JsonProperty("Password")]
        public string? Password { get; set; }
    }
}
