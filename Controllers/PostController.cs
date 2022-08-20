using social_api.Models;
using Microsoft.AspNetCore.Mvc;
using social_api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace social_api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostRepository _postRepository;

    public PostController(ILogger<PostController> logger, IPostRepository repository)
    {
        _logger = logger;
        _postRepository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Post>> GetAllPosts()
    {
        return Ok(_postRepository.GetAllPosts());
    }

    [HttpGet]
    [Route("{postId:int}")]
    public ActionResult<Post> GetPostById(int postId)
    {
        var post = _postRepository.GetPostById(postId);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Post> CreatePost(Post newPost)
    {
        if (HttpContext.User == null)
        {
            return Unauthorized();
        }

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        var userId = int.Parse(userIdClaim.Value);

        if (userId == null)
        {
            return Unauthorized();
        }

        newPost.OwnerId = userId;

        if (!ModelState.IsValid || newPost == null)
        {
            return BadRequest();
        }

        var createdPost = _postRepository.CreatePost(newPost);
        return Created(nameof(GetPostById), createdPost);
    }

    [HttpPut]
    [Route("{postId:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Post> UpdatePost(Post newPost)
    {
        if (HttpContext.User == null)
        {
            return Unauthorized();
        }

        Console.WriteLine(newPost);

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        var userId = int.Parse(userIdClaim.Value);

        if (!ModelState.IsValid || newPost == null)
        {
            return BadRequest();
        }

        Console.WriteLine(userId);
        Console.WriteLine(newPost.OwnerId);

        if (userId == newPost.OwnerId)
        {
            return Ok(_postRepository.UpdatePost(newPost));
        }
        else
        {
            return Unauthorized();
        }
    }

    [HttpDelete]
    [Route("{postId:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult DeletePost(int postId)
    {
        if (HttpContext.User == null)
        {
            return Unauthorized();
        }

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        var userId = int.Parse(userIdClaim.Value);
        var postToDelete = _postRepository.GetPostById(postId);

        if (userId == null)
        {
            return Unauthorized();
        }

        if (userId == postToDelete.OwnerId)
        {
            _postRepository.DeletePostById(postId);
            return NoContent();
        }
        else
        {
            return Unauthorized();
        }
    }
}