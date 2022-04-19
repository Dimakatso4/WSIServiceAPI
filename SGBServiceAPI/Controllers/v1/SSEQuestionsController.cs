using AutoMapper.Configuration;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WSIServiceAPI.Models;
using WSIServiceAPI.Services;

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SSEQuestionsController : ControllerBase
    {
       // private IConfiguration _config;
        private readonly IDapper _dapper;

        public SSEQuestionsController(IDapper dapper)
        {
           // _config = config;
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateSSEQuestions))]
        public async Task<int> CreateSSEQuestions(SSEQuestionsModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@SSEQuestionID", data.SSEQuestionID, DbType.Int32);
            dataBaseParams.Add("@Question", data.Question);
            dataBaseParams.Add("@Answer", data.Answer);
            dataBaseParams.Add("@InstrumentId", data.InstrumentId, DbType.Int32);


            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddSSEQuestions]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("SSEQuestionID");
            return returnVal;
        }
        [HttpGet(nameof(GetAllKPIQuestion))]
        public Task<List<SSEQuestionsModel>> GetAllKPIQuestion()
        {
            /*var Output = Task.FromResult(_dapper.GetAll<SSEComponentModel>($"select * from [dbo].[tblAreaOfEvaluation]", null,
                    commandType: CommandType.Text));*/

            return Task.FromResult(_dapper.GetAll<SSEQuestionsModel>($"select * from [dbo].[tblKPIQuestion]", null,
                    commandType: CommandType.Text));

        }
        [HttpGet(nameof(GetKPIQuestionByID))]
        public Task<List<SSEQuestionsModel>> GetKPIQuestionByID(int CompID, int areaOfEvaluationID)
        {
            var ComponentList = Task.FromResult(_dapper.GetAll<SSEQuestionsModel>($"select * from [dbo].tblKPIQuestion where [CompID] = {CompID} or  [areaOfEvaluationID] = {areaOfEvaluationID}", null,
                    commandType: CommandType.Text));
            return ComponentList;
        }
    }
}
