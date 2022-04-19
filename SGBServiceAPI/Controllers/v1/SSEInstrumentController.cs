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
    public class SSEInstrumentController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SSEInstrumentController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateSSEInstrument))]
        public async Task<int> CreateSSEInstrument(SSEInstrumentModel data)
        {
            var dataBaseParams = new DynamicParameters();

            dataBaseParams.Add("@AreaOfEvalutionID", data.AreaOfEvaluationID);
            dataBaseParams.Add("@CreatedBy", data.CreatedBy);
            dataBaseParams.Add("@IntrumentName", data.InstrumentName);
            dataBaseParams.Add("@SSEStatusID", data.SSEStatusID);
            dataBaseParams.Add("@Year", data.Year);
            dataBaseParams.Add("@BusinessUnitID", data.BusinessUnit);
            dataBaseParams.Add("@KPI", data.kpiID);

            dataBaseParams.Add("@InstrumentId", 0, DbType.Int32, ParameterDirection.Output);


            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spSSEItrumentAdd]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("InstrumentId");
            return returnVal;
        }

        [HttpPost(nameof(CreateSSESelectedKPI))]
        public async Task<int> CreateSSESelectedKPI(SSEInstrumentModel data)
        {
            var dataBaseParams = new DynamicParameters();

            dataBaseParams.Add("@InstrumentId", data.SSEInstrumentId);
            dataBaseParams.Add("@KPIID", data.kpiID);

            //dataBaseParams.Add("@InstrumentId", 0, DbType.Int32, ParameterDirection.Output);


            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spKPISelectedAdd]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("InstrumentId");
            return returnVal;
        }


        [HttpGet(nameof(GetSSEInstrumentAll))]
        public Task<List<SSEInstrumentModel>> GetSSEInstrumentAll()
        {
            var Output = Task.FromResult(_dapper.GetAll<SSEInstrumentModel>($"Select * from [dbo].[tblSSEIntrument] sse Inner Join [dbo].[tblAreaOfEvaluation] area on area.areaOfEvaluationID = sse.AreaOfEvaluationID inner join [dbo].[tblSSEStatus] sta on sta.SSEStatusID = sse.SSEStatusID", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetSSEInstrumentLastID))]
        public Task<List<SSEInstrumentModel>> GetSSEInstrumentLastID()
        {
            var Output = Task.FromResult(_dapper.GetAll<SSEInstrumentModel>($"select MAX(SSEInstrumentID) as instrumentId from [dbo].[tblSSEIntrument]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetSSEInstrumentById))]
        public Task<List<SSEInstrumentModel>> GetSSEInstrumentById(int Id)
        {
            var Output = Task.FromResult(_dapper.GetAll<SSEInstrumentModel>($"select * from [dbo].[tblSSEInstrument] where InstrumentId={Id}", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpPatch(nameof(UpdateSSEInstrument))]
        public Task<int> UpdateSSEInstrument(SSEInstrumentModel data, int PlanID)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@InstrumentId", data.SSEInstrumentId, DbType.Int32);
            //dataBaseParams.Add("@AreaOfEvaluation", data.AreaOfEvaluation);
            //dataBaseParams.Add("@Component", data.Component);
            //dataBaseParams.Add("@InstrumentName", data.InstrumentName);
            //dataBaseParams.Add("@QuestionKPI", data.QuestionKPI);

            var updateInstrument = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSSEInstrument]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateInstrument;

        }

        [HttpDelete(nameof(DeleteSSEIntrument))]
        public async Task<int> DeleteSSEIntrument(int InstrumentID)
        {
            var Output = await Task.FromResult(_dapper.Execute($"Delete From [dbo].[tblSSEIntrument] Where [SSEInstrumentID] = {InstrumentID}", null, commandType: CommandType.Text));
            return Output;
        }


    }
}
