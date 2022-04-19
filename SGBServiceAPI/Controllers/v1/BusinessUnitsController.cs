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
    public class BusinessUnitsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public BusinessUnitsController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateBusinessUnit))]
        public async Task<int> CreateBusinessUnit(BusinessUnitsModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@BusinessUnit", data.BusinessUnit);


            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddBusinessUnits]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("@BusinessUnitID");
            return returnVal;
        }
        [HttpGet(nameof(GetBusinessUnits))]
        public Task<List<BusinessUnitsModel>> GetBusinessUnits()
        {
            var Output = Task.FromResult(_dapper.GetAll<BusinessUnitsModel>($"select * from [dbo].[tblBusinessUnits]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetBusinessUnitsById))]
        public Task<List<BusinessUnitsModel>> GetBusinessUnitsById(int Id)
        {
            var Output = Task.FromResult(_dapper.GetAll<BusinessUnitsModel>($"select * from [dbo].[tblBusinessUnits] where BusinessUnitID={Id}", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpDelete(nameof(DeleteBusinessUnits))]
        public async Task<int> DeleteBusinessUnits(int UnitId)
        {
            var Output = await Task.FromResult(_dapper.Execute($"Delete From [dbo].[tblBusinessUnits] Where BusinessUnitID = {UnitId}", null, commandType: CommandType.Text));
            return Output;
        }
        [HttpPost(nameof(UpdateBusinessUnits))]
        public Task<int> UpdateBusinessUnits(BusinessUnitsModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@BusinessUnitID", data.BusinessUnitID, DbType.Int32);
            dataBaseParams.Add("@BusinessUnit", data.BusinessUnit);
 

            var updateBU = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateBusinessUnits]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateBU;
        }
    }
}
