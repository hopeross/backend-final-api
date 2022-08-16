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
    public string OwnerId { get; set; }

    [Required]
    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }
    public string DeletedOn { get; set; }

    public string CommentId { get; set; }
}