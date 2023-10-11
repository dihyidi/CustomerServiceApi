using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CustomerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CustomerService.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration config;

    public LoginController(IConfiguration config)
    {
        this.config = config;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody]LoginModel login)
    {
        IActionResult response = Unauthorized();
        var isAuthenticated = AuthenticateUser(login);

        if (!isAuthenticated)
        {
            return response;
        }
        
        var tokenString = GenerateJsonWebToken();
        response = Ok(new { token = tokenString });

        return response;
    }

    private string GenerateJsonWebToken()
    {
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];
        var expiry = DateTime.Now.AddMinutes(120);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: issuer, audience: audience,
            expires: expiry, signingCredentials: credentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }

    private bool AuthenticateUser(LoginModel login)
    {
        // demo purpose
        return login.Login == config["Auth:Login"] && login.Password == config["Auth:Pwd"];
    }   
}