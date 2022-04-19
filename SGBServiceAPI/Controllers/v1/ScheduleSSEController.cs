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
    public class ScheduleSSEController : ControllerBase
    {
        private readonly IDapper _dapper;
        public ScheduleSSEController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(ScheduleSSE))]
        public async Task<int> ScheduleSSE(ScheduleSSEModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@SSEID", data.SSEID, DbType.Int32);
            dataBaseParams.Add("@District", data.District);
            dataBaseParams.Add("@School", data.School);
            dataBaseParams.Add("@StartDate", data.StartDate, DbType.DateTime);
            dataBaseParams.Add("@EndDate", data.EndDate, DbType.DateTime);
          
            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddScheduleSSE]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("SSEID");
            return returnVal;
        }
        [HttpGet(nameof(GetScheduledSSE))]
        public Task<List<ScheduleSSEModel>> GetScheduledSSE()
        {
            var Output = Task.FromResult(_dapper.GetAll<ScheduleSSEModel>($"select * from [dbo].[tblScheduleSSE]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetScheduledSSEById))]
        public Task<List<ScheduleSSEModel>> GetScheduledSSEById(int Id)
        {
            var Output = Task.FromResult(_dapper.GetAll<ScheduleSSEModel>($"select * from [dbo].[tblScheduleSSE] where SSEID={Id}", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetDistrictName))]
        public Task<List<ScheduleSSEModel>> GetDistrictName()
        {
            var Output = Task.FromResult(_dapper.GetAll<ScheduleSSEModel>($"select [District] from [dbo].[tblScheduleSSE]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetSchoolName))]
        public Task<List<ScheduleSSEModel>> GetSchoolName()
        {
            var Output = Task.FromResult(_dapper.GetAll<ScheduleSSEModel>($"select [School] from [dbo].[tblScheduleSSE]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpPatch(nameof(updateScheduledSSE))]
        public Task<int> updateScheduledSSE(ScheduleSSEModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@SSEID", data.SSEID, DbType.Int32);
            dataBaseParams.Add("@District", data.District);
            dataBaseParams.Add("@School", data.School);
            dataBaseParams.Add("@StartDate", data.StartDate, DbType.DateTime);
            dataBaseParams.Add("@EndDate", data.EndDate, DbType.DateTime);

            var updateScheduledSSE = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateScheduleSSE]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateScheduledSSE;

        }
    }
}
