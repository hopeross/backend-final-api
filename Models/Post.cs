using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace social_api.Models;

public class Post
{
    [JsonIgnore]
    public int PostId { get; set; }

    [Required]
    public string PostText {get; set; }

    [Required]
    public string OwnerId { get; set; } = "1";

    [Required]
    public string CreatedOn { get; set; } = DateTime.Now.ToString();

    public string? UpdatedOn { get; set; } = DateTime.Now.ToString();
    public string? DeletedOn { get; set; } = DateTime.Now.ToString();

    public string? CommentId { get; set; }
}