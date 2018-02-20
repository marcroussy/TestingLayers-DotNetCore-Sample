using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Supermarket.WebApi.Models;

namespace Supermarket.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        public IConfiguration _configuration { get; }

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IActionResult Post([FromBody]LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Username))
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loginRequest.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SigningKey"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return new OkObjectResult(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
