
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WSIServiceAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using WSIServiceAPI.Services;
using System.Threading.Tasks;
using System.Data;

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /*private IConfiguration _config;
        private readonly IDapper _dapper;

        public LoginController(IDapper dapper, IConfiguration config)
        {
            _config = config;
            _dapper = dapper;
        }

        [AllowAnonymous]
        [HttpPost("AuthenticateParent")]
        public IActionResult AuthenticateParent([FromBody] ParentModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUserAsync(login);

            if (user.Result != null)
            {
                var tokenString = GenerateJSONWebToken(user.Result);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(ParentModel userInfo)
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ParentModel> AuthenticateUserAsync(ParentModel login)
        {
            ParentModel user = null;
            var result = await Task.FromResult(_dapper.Get<ParentModel>($"Select IDNumber,Tel3 MobileNo from [tblParent_info] where IDNumber = '{login.IDNumber}' and Tel3 = '{login.MobileNo}'", null, commandType: CommandType.Text));
            
            if(result == null)
                return null;

            //Validate the User Credentials    
            if (login.IDNumber == result.IDNumber && login.MobileNo == result.MobileNo)
            {
                user = result;
            }
            return user;
        }*/
    }
}
