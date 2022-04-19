using Microsoft.AspNetCore.Mvc;
using WSIServiceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using WSIServiceAPI.Models;
using System.Data;

namespace WSIServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        /*private readonly IDapper _dapper;
        public UserController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(UserModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", data.Id, DbType.Int32);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Add_Article]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        [HttpGet(nameof(UserList))]
        public Task<List<UserModel>> UserList()
        {
            var result = Task.FromResult(_dapper.GetAll<UserModel>($"select * from [tblUsers]", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetListOfHeadOfficeUsers))]
        public Task<List<UserModel>> GetListOfHeadOfficeUsers()
        {
            var result = Task.FromResult(_dapper.GetAll<UserModel>($"select * from [tblUsers] where RoleId = 2", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetListOfPrincipals))]
        public Task<List<UserModel>> GetListOfPrincipals()
        {
            var result = Task.FromResult(_dapper.GetAll<UserModel>($"select * from [tblUsers] where RoleId = 4", null,
                    commandType: CommandType.Text));
            return result;
        }
        //[HttpGet(nameof(GetListOfProvincialUsers))]
        //public Task<List<UserModel>> GetListOfProvincialUsers()
        //{
        //    var result = Task.FromResult(_dapper.GetAll<UserModel>($"select * from [tblUsers] where RoleId = 1", null,
        //            commandType: CommandType.Text));
        //    return result;
        //}
        [HttpGet(nameof(GetListOfDistrictUsers))]
        public Task<List<UserModel>> GetListOfDistrictUsers()
        {
            var result = Task.FromResult(_dapper.GetAll<UserModel>($"select * from [tblUsers] where RoleId = 3", null,
                    commandType: CommandType.Text));
            return result;
        }
        //[HttpGet(nameof(GetListOfSchoolUsers))]
        //public Task<List<UserModel>> GetListOfSchoolUsers()
        //{
        //    var result = Task.FromResult(_dapper.GetAll<UserModel>($"select * from [tblUsers] where RoleId = 1", null,
        //            commandType: CommandType.Text));
        //    return result;
        //}
        [HttpGet(nameof(GetById))]
        public async Task<UserModel> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where UserId = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute($"Delete [tblUsers] Where UserId = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(Count))]
        public Task<int> Count(int num)
        {
            var totalcount = Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [tblUsers] WHERE SchoolId like '%{num}%'", null,
                    commandType: CommandType.Text));
            return totalcount;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(UserModel data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", data.Id);
            dbPara.Add("Name", data.Firstname, DbType.String);

            var updateArticle = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Article]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }

        [HttpPatch(nameof(UpdateParent))]
        public Task<int> UpdateParent(UserModel data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", data.Id);
            dbPara.Add("Cell", data.CellNumber);

            var updateParent = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateUser]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateParent;
        }*/
    }
}
