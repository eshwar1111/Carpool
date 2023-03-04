using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Carpool.Database;
using Carpool.Models;
using Carpool.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Carpool.Api.Controllers
{

    [Route("api/authentication")]
    [ApiController]

    public class AuthenticationController : Controller
    {
        private readonly DatabaseContext CarpoolDb;
        private readonly IAuthService auth;
        private readonly IConfiguration configuration;
        public AuthenticationController(DatabaseContext CarpoolDb, IAuthService auth, IConfiguration configuration)   
        {
            this.configuration = configuration;
            this.CarpoolDb = CarpoolDb;
            this.auth = auth;
        }

        
        [HttpGet]
        [Route("getusers")]
        public async Task<IActionResult> Getusers()
        {
            try
            {
                return Ok(await CarpoolDb.Users.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(SignUpCredential signUp)
        {
            try
            {
                if (!auth.IsSignedUp(signUp.UserName, signUp.Password))
                {
                    if (await auth.SignUp(signUp))
                    {
                        return Ok("Successful");
                    }
                }
                return BadRequest("passwords not matching");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> LogIn(LogInCredential logIn)
        {
            try
            {
                int userId=auth.CheckPassword(logIn.UserName, logIn.Password);
                if (auth.IsSignedUp(logIn.UserName, logIn.Password) && userId!=-1)
                {
                    List<Claim> claims = new List<Claim>{
                    new Claim("Name",logIn.UserName),
                    new Claim("Sid",userId.ToString())
             
                    };

                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

                    var Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                    var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: Credentials
                        );
                    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwt;
                }
                return "false";
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
