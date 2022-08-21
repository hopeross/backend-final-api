using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace social_api.Models;

public class Post
{
    public int PostId { get; set; }

    [Required]
    public string PostText {get; set; }

    [Required]
    public int OwnerId { get; set; }

    //TODO This works but is this the right place to do assignment?
    [Required]
    public string CreatedOn { get; set; } = DateTime.Now.ToString();

    public string? CommentId { get; set; }
}