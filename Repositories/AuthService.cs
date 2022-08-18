using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using social_api.Migrations;
using social_api.Models;
using Microsoft.IdentityModel.Tokens;
using bcrypt = BCrypt.Net.BCrypt;

namespace social_api.Repositories;

public class AuthService : IAuthService
{
    private static SocialDbContext _context;
    private static IConfiguration _config;

    public AuthService(SocialDbContext context, IConfiguration config) {
        _context = context;
        _config = config;
    }

    public User CreateUser(User user)
    {
        var passwordHash = bcrypt.HashPassword(user.Password);
        user.Password = passwordHash;
        
        _context.Add(user);
        _context.SaveChanges();
        return user;
    }

    public string SignIn(string email, string password)
    {
        var user = _context.Users.SingleOrDefault(x => x.Email == email);
        var verified = false;

        if (user != null) {
            verified = bcrypt.Verify(password, user.Password);
        }

        if (user == null || !verified)
        {
            return String.Empty;
        }
        
        // Create & return JWT
        return BuildToken(user);
    }

    public User UpdateUser(User newUser)
    {
        var originalUser = _context.Users.Find(newUser);
        if (originalUser != null) {
            originalUser.FirstName = newUser.FirstName;
            originalUser.LastName = newUser.LastName;
            originalUser.Location = newUser.Location;
            originalUser.Title = newUser.Title;
        }
        _context.SaveChanges();
        return originalUser;
    }

    public User GetUserById(int userId)
    {
        return _context.Users.FirstOrDefault(u => u.UserId == userId);
    }

    private string BuildToken(User user) {
        var secret = _config.GetValue<String>("TokenSecret");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        
        // Create Signature using secret signing key
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        // Create claims to add to JWT payload
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim("UserId", user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? "")
        };

        // Create token
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signingCredentials);
        
        // Encode token
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}