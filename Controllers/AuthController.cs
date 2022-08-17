using social_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using social_api.Models;

namespace social_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService service)
    {
        _logger = logger;
        _authService = service;
    }

    [HttpPost]
    [Route("signup")]
    public ActionResult CreateUser(User user)
    {
        if (user == null || !ModelState.IsValid)
        {
            return BadRequest();
        }
        _authService.CreateUser(user);
        return NoContent();
    }

    [HttpGet]
    [Route("signin")]
    public ActionResult<string> SignIn(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return BadRequest();
        }

        var token = _authService.SignIn(email, password);

        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpGet]
    [Route("{userId:int}")]
    public ActionResult<User> GetUserById(int userId)
    {
        var user = _authService.GetUserById(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut]
    [Route("{userId:int}")]
    public ActionResult<Post> UpdateUser(User user)
    {
        if (!ModelState.IsValid || user == null)
        {
            return BadRequest();
        }
        return Ok(_authService.UpdateUser(user));
    }
}