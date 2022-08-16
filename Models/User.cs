using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace social_api.Models;

public class User 
{
    [JsonIgnore]
    public int UserId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Location { get; set; }
    public string? Title { get; set; }
}