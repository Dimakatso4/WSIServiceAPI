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

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IDapper _dapper;
        public RolesController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateRole))]
        public async Task<int> CreateRole(RolesModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@RoleId", data.RoleId, DbType.Int32);
            dataBaseParams.Add("@Role", data.Role);
            dataBaseParams.Add("@RoleDescription", data.RoleDescription);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddRoles]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("RoleId");
            return returnVal;
        }
        [HttpGet(nameof(GetRole))]
        public Task<List<RolesModel>> GetRole()
        {
            var Output = Task.FromResult(_dapper.GetAll<RolesModel>($"select * from [dbo].[tblRoles]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetRoleById))]
        public Task<List<RolesModel>> GetRoleById(int Id)
        {
            var Output = Task.FromResult(_dapper.GetAll<RolesModel>($"select * from [dbo].[tblRoles] where RoleId={Id}", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpDelete(nameof(DeleteRoles))]
        public async Task<int> DeleteRoles(int RoleId)
        {
            var Output = await Task.FromResult(_dapper.Execute($"DELETE FROM [dbo].[tblRoles] WHERE [RoleId] = {RoleId}", null, commandType: CommandType.Text));
            return Output;
        }
        [HttpPatch(nameof(UpdateRole))]
        public Task<int> UpdateRole(RolesModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@RoleId", data.RoleId, DbType.Int32);
            dataBaseParams.Add("@Role", data.Role);
            dataBaseParams.Add("@RoleDescription", data.RoleDescription);

            var updateR = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateRole]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateR;
        }
    }
}
