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
    public class ManageLegislationController : ControllerBase
    {
        private readonly IDapper _dapper;
        public ManageLegislationController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateLigislation))]
        public async Task<int> CreateLigislation(ManageLegislationModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@LegislationID", data.LegislationID, DbType.Int32);
            dataBaseParams.Add("@Description", data.Description, DbType.String);
            dataBaseParams.Add("@Description", data.Description);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddBusinessUnit]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("UnitId");
            return returnVal;
        }
        [HttpGet(nameof(GetLegislationAll))]
        public Task<List<ManageLegislationModel>> GetLegislationAll()
        {
            var Output = Task.FromResult(_dapper.GetAll<ManageLegislationModel>($"Select * from [dbo].[tblManageLegislation]", null,
                    commandType: CommandType.Text));
            return Output;
        }

      
    }
}
