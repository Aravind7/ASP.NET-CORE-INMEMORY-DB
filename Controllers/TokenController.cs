using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EF_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EF_Demo.Controllers
{
    [Route("api/[controller]")]
    public class TokenController: Controller
    {
        private IConfiguration _config;
    public TokenController(IConfiguration config)
    {
      _config = config;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult CreateToken([FromBody]Models.LoginModel login)
    {
      IActionResult response = Unauthorized();
      var user = Authenticate(login);

      if (user != null)
      {
        var tokenString = BuildToken(user);
        response = Ok(new { token = tokenString });
      }

      return response;
    }


    private string BuildToken(UserModel user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          expires: DateTime.Now.AddMinutes(2),
          signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
     }

     private UserModel Authenticate(Models.LoginModel login)
     {
        UserModel user = null;

        if (login.Username == "mario" && login.Password == "secret")
        {
            user = new UserModel { Name = "Mario Rossi", Email = "mario.rossi@domain.com"};
        }
        return user;
     }

    }
}