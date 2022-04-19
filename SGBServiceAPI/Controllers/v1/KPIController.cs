using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WSIServiceAPI.Models;
using WSIServiceAPI.Services;

namespace WSIServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KPIController : ControllerBase
    {
        private readonly IDapper _dapper;

        public KPIController(IDapper dapper)
        {
            _dapper = dapper;

        }

        [HttpPost(nameof(AddKPI))]
        public async Task<int> AddKPI(KPIModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", data.Id, DbType.Int32);
            dbparams.Add("@AreaOfEvaulation", data.AreaOfEvaulation);
            dbparams.Add("@Component", data.Component);
            dbparams.Add("@Kpi", data.Kpi);
            dbparams.Add("@OptionalCompulsory", data.OptionalCompulsory);
            dbparams.Add("@Rating", data.Rating);
            dbparams.Add("@Legislation", data.Legislation);
            dbparams.Add("@Description", data.Description);
            dbparams.Add("@BusinessUnit", data.BusinessUnit);
            dbparams.Add("@Resource", data.Resource);
            dbparams.Add("@KpiAuditTrail", data.KpiAuditTrail);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddKPI]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id");
            return retVal;
        }

        [HttpPatch(nameof(UpdateKPI))]
        public Task<int> UpdateKPI(KPIModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", data.Id, DbType.Int32);
            dbparams.Add("@AreaOfEvaulation", data.AreaOfEvaulation);
            dbparams.Add("@Component", data.Component);
            dbparams.Add("@Kpi", data.Kpi);
            dbparams.Add("@OptionalCompulsory", data.OptionalCompulsory);
            dbparams.Add("@Rating", data.Rating);
            dbparams.Add("@Legislation", data.Legislation);
            dbparams.Add("@Description", data.Description);
            dbparams.Add("@BusinessUnit", data.BusinessUnit);
            dbparams.Add("@Resource", data.Resource);
            dbparams.Add("@KpiAuditTrail", data.KpiAuditTrail);
            var updateKPI = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateKPI]",
               dbparams,
               commandType: CommandType.StoredProcedure));
            return updateKPI;
        }

        [HttpGet(nameof(GetKPIList))]
        public Task<List<KPIModel>> GetKPIList()
        {
            var result = Task.FromResult(_dapper.GetAll<KPIModel>($"Select * from [dbo].[tblKPIQuestions]", null,
            commandType: CommandType.Text));
            return result;
        }
    }


}
