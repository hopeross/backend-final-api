using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace social_api.Models;

public class Comment {
    [JsonIgnore]
    public int CommentId {get; set;}
    
    [Required]
    public string CommentText { get; set; }

    [Required]
    public int OwnerId { get; set; }

    [Required]
    public string CreatedOn { get; set; }

    public string? UpdatedOn { get; set; }
    public string? DeletedOn { get; set; }
}