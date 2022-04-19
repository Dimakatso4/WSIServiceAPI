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
using System.Collections.Generic;
using System.Linq;

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IDapper _dapper;

        public LoginUserController(IDapper dapper, IConfiguration config)
        {
            _config = config;
            _dapper = dapper;
        }

        [AllowAnonymous]
        [HttpPost("AuthenticateUser")]
        public Task<TUsersModel> AuthenticateUser([FromBody] TUsersModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUserAsync(login);

            if (user.Result != null)
            {
                var tokenString = GenerateJSONWebToken(user.Result);
                response = Ok(new { token = tokenString });
            }

            return user;
        }
       


        private string GenerateJSONWebToken(TUsersModel userInfo)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<TUsersModel> AuthenticateUserAsync(TUsersModel login)
        {
            TUsersModel user = null;


            user = await Task.FromResult(_dapper.Get<TUsersModel>($"Select *  from dbo.tblUsers  where Username = '{login.Username}' and Password = '{login.Password}' and UserActive = 'Active'", null, commandType: CommandType.Text));
            return user;


        }


        private async Task<TUsersModel> AuthenticatingUserAsync(TUsersModel login)
        {
            TUsersModel user = null;

            if (!string.IsNullOrEmpty(login.Persal))
            {
                user = await Task.FromResult(_dapper.Get<TUsersModel>($"Select  Persal,UserId,FirstName,Surname,IDNumber,tblusers.Passport,tblusers.Position,UserActive,tblUserRole.rolename ,tblUserRole.RoleId from dbo.tblUsers join dbo.tblUserRole on tblUsers.Position = tblUserRole.RoleName where  Persal = '{login.Persal}'  and Password = '{login.Password}' and UserActive = 'Active'", null, commandType: CommandType.Text));
            }
            else if (!string.IsNullOrEmpty(login.IDNumber))
            {
                user = await Task.FromResult(_dapper.Get<TUsersModel>($"Select  Persal,UserId,FirstName,Surname,IDNumber,tblusers.Passport,tblusers.Position,UserActive,tblUserRole.rolename ,tblUserRole.RoleId from dbo.tblUsers join dbo.tblUserRole on tblUsers.Position = tblUserRole.RoleName where  IDNumber = '{login.IDNumber}' and Password = '{login.Password}' and UserActive = 'Active'", null, commandType: CommandType.Text));
            }
            else if (!string.IsNullOrEmpty(login.Passport))
            {
                user = await Task.FromResult(_dapper.Get<TUsersModel>($"Select  Persal,UserId,FirstName,Surname,IDNumber,tblusers.Passport,tblusers.Position,UserActive,tblUserRole.rolename ,tblUserRole.RoleId from dbo.tblUsers join dbo.tblUserRole on tblUsers.Position = tblUserRole.RoleName where Passport = '{login.Passport}' and Password = '{login.Password}' and UserActive = 'Active'", null, commandType: CommandType.Text));
            }
            else if (!string.IsNullOrEmpty(login.Username))
            {
                user = await Task.FromResult(_dapper.Get<TUsersModel>($"Select *  from dbo.tblUsers  where Username = '{login.Username}' and Password = '{login.Password}' and UserActive = 'Active'", null, commandType: CommandType.Text));
            }


            return user; 

            {




            }

        }

        public static string GenerateRandomAlphanumericString(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!$%_@^*()";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }
    }
}
