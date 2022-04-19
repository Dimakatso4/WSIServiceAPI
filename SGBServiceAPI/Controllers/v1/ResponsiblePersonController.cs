using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using WSIServiceAPI.Models;
using System.Data;
using WSIServiceAPI.Services;

namespace WSIServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsiblePersonController : ControllerBase
    {
       /* public IActionResult Index()
        {
            return View();
        }*/
        private readonly IDapper _dapper;
        public ResponsiblePersonController(IDapper dapper)
        {
            _dapper = dapper;
        }
        //Leave out
        [HttpPost(nameof(CreateResponsiblePerson))]
        public async Task<int> CreateResponsiblePerson(ResponsibilityModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@ID", data.ID, DbType.Int32);
            dataBaseParams.Add("@Responsibility", data.Responsibility);
            dataBaseParams.Add("@RoleId", data.RoleID, DbType.Int32);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddResponsiblePerson]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var Resp = dataBaseParams.Get<int>("ID");
            return Resp;
        }
        [HttpGet(nameof(GetResponsiblePersonList))]
        public Task <List<UserRoleModel>> GetResponsiblePersonList()
        {
            var Resp = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from [dbo].[tblUserRole]", null,
                    commandType: CommandType.Text));
            return Resp;
        }

        [HttpGet(nameof(GetOfficeLevel))]
        public Task<List<UserRoleModel>> GetOfficeLevel(string Officelevel)
        {
            var Resp = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from [dbo].[tblUserRole] where [Officelevel] = '{Officelevel}' ", null,
                    commandType: CommandType.Text));
            return Resp;
        }

        [HttpGet(nameof(GetResponsiblePersonNames))]
        public Task<List<UserRoleModel>> GetResponsiblePersonNames()
        {
            var Resp = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select [rolename] from [dbo].[tblUserRole]", null,
                    commandType: CommandType.Text));
            return Resp;
        }
        [HttpGet(nameof(GetResponsiblePersonByID))]
        public Task<List<UserRoleModel>> GetResponsiblePersonByID(int ID)
        {
            var Resp = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select [rolename] from [dbo].[tblUserRole] where [Id] = {ID}", null,
                    commandType: CommandType.Text));
            return Resp;
        }

    }
}
