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
    public class RateKPIController : ControllerBase
    {
        private readonly IDapper _dapper;
        public RateKPIController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateKPIRating))]
        public async Task<int> CreateKPIRating(RateKPIModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@KPIId", data.KPIId, DbType.Int32);
            dataBaseParams.Add("@Rating", data.Rating, DbType.Int32);
            dataBaseParams.Add("@YesNo", data.YesNo);
            dataBaseParams.Add("@NA", data.NA);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddRateKPI]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("KPIId");
            return returnVal;
        }
        [HttpGet(nameof(GetKPIRatingList))]
        public Task<List<RateKPIModel>> GetKPIRatingList()
        {
            var Output = Task.FromResult(_dapper.GetAll<RateKPIModel>($"select * from [dbo].[tblRateKPI]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetKPIRatingListById))]
        public Task<List<RateKPIModel>> GetKPIRatingListById(int Id)
        {
            var Output = Task.FromResult(_dapper.GetAll<RateKPIModel>($"select * from [dbo].[tblRateKPI] where KPIId={Id}", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpDelete(nameof(DeleteKPIRating))]
        public async Task<int> DeleteKPIRating(int KPIId)
        {
            var Output = await Task.FromResult(_dapper.Execute($"Delete From [dbo].[tblRateKPI] Where KPIId = {KPIId}", null, commandType: CommandType.Text));
            return Output;
        }
        [HttpPatch(nameof(UpdateKPIRating))]
        public Task<int> UpdateKPIRating(RateKPIModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@KPIId", data.KPIId, DbType.Int32);
            dataBaseParams.Add("@Rating", data.Rating, DbType.Int32);
            dataBaseParams.Add("@YesNo", data.YesNo);
            dataBaseParams.Add("@NA", data.NA);


            var updateBU = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateRateKPI]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateBU;
        }
    }
}
