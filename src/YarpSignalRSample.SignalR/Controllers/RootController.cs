using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace YarpSignalRSample.SignalR.Controllers;

[Route("api")]
[ApiController]
public class RootController : ControllerBase
{
    [HttpGet("token")]
    public IActionResult GetToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.Default.GetBytes("secretAccessTokenKey_1231");

        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddMinutes(20),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256));

        var token = tokenHandler.WriteToken(jwtToken);
        
        return Ok(token);
    }
}