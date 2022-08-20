using social_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using social_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

    //Authorization first

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
    //TODO: Learn how to do this more gooder because this transmits passwords unencrypted 
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

    //User Operations Second.  Should have split this into a different controller.

    [HttpGet]
    [Route("current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> GetCurrentUser()
    {
        if (HttpContext.User == null)
        {
            return Unauthorized();
        }

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        var userId = Int32.Parse(userIdClaim.Value);
        var user = _authService.GetUserById(userId);

        if (user == null)
        {
            return Unauthorized();
        }

        return Ok(user);
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> UpdateUser(User editUser)
    {
        if (HttpContext.User == null) {
            return Unauthorized();
        }

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        var userId = Int32.Parse(userIdClaim.Value);

        if (!ModelState.IsValid || editUser == null)
        {
            return BadRequest();
        }

        if (userId == editUser.UserId) {
            return Ok(_authService.UpdateUser(editUser));   
        } else {
            return Unauthorized();
        }
    }
}