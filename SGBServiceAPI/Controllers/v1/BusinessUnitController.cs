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
    public class BusinessUnitController : ControllerBase
    {
        private readonly IDapper _dapper;
        public BusinessUnitController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateBusinessUnit))]
        public async Task<int> CreateBusinessUnit(BusinessUnitModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@UnitId", data.UnitId, DbType.Int32);
            dataBaseParams.Add("@UnitName", data.UnitName);
            dataBaseParams.Add("@Description", data.Description);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddBusinessUnit]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("UnitId");
            return returnVal;
        }
        [HttpGet(nameof(GetBusinessUnit))]
        public Task<List<BusinessUnitModel>> GetBusinessUnit()
        {
            var Output = Task.FromResult(_dapper.GetAll<BusinessUnitModel>($"select * from [dbo].[tblBusinessUnit]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetBusinessUnitById))]
        public Task<List<BusinessUnitModel>> GetBusinessUnitById(int Id)
        {
            var Output = Task.FromResult(_dapper.GetAll<BusinessUnitModel>($"select * from [dbo].[tblBusinessUnit] where UnitId={Id}", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpDelete(nameof(DeleteBusinessUnit))]
        public async Task<int> DeleteBusinessUnit(int UnitId)
        {
            var Output = await Task.FromResult(_dapper.Execute($"Delete From [dbo].[tblBusinessUnit] Where UnitId = {UnitId}", null, commandType: CommandType.Text));
            return Output;
        }
        [HttpPatch(nameof(UpdateBusinessUnit))]
        public Task<int> UpdateBusinessUnit(BusinessUnitModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@UnitId", data.UnitId, DbType.Int32);
            dataBaseParams.Add("@UnitName", data.UnitName);
            dataBaseParams.Add("@Description", data.Description);

            var updateBU = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateBusinessUnit]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateBU;
        }
    }
}
