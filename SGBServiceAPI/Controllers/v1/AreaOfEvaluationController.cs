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
    public class AreaOfEvaluationController : ControllerBase
    {
        private readonly IDapper _dapper;
        public AreaOfEvaluationController(IDapper dapper)
        {
            _dapper = dapper;

        }
        [HttpPost(nameof(CreateAreaOfEvalution))]
        public async Task<int> CreateAreaOfEvalution(AreaOfEvaluationModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ManageAreaOfEvalutionID", data.ManageAreaOfEvalutionID, DbType.Int32);
            dbparams.Add("@FocusArea", data.FocusArea);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddAreaOfEvaluation]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@ManageAreaOfEvalutionID");
            return retVal;
        }

        [HttpGet(nameof(GetAreaOfEvaluationList))]
        public Task<List<AreaOfEvaluationModel>> GetAreaOfEvaluationList()
        {
            var ArealOfEvaluationResult = Task.FromResult(_dapper.GetAll<AreaOfEvaluationModel>($"Select * from [dbo].[tblManageAreaOfEvaluation] ", null,
            commandType: CommandType.Text));

            foreach (AreaOfEvaluationModel areaofEvalutionModel in ArealOfEvaluationResult.Result)
            {

                var managementComponentList = Task.FromResult(_dapper.GetAll<SSEComponentModel>($"SELECT ManageComponentID,ComponentName FROM [dbo].[tblManageComponents] WHERE ManageAreaOfEvaluationID = {areaofEvalutionModel.ManageAreaOfEvalutionID}", null,
                   commandType: CommandType.Text));

                areaofEvalutionModel.SSEComponents = managementComponentList.Result;

                foreach (SSEComponentModel sseComModel in areaofEvalutionModel.SSEComponents)
                {

                    var managementKPIList = Task.FromResult(_dapper.GetAll<ManageKPIModel>($"SELECT ManageKPIID,KPIName FROM [dbo].[tblManageKPI] WHERE ManageComponentID = {sseComModel.ManageComponentID}", null,
                       commandType: CommandType.Text));

                    sseComModel.ManageKPIs = managementKPIList.Result;

                }

            }

            return ArealOfEvaluationResult;
        }

        [HttpGet(nameof(GetAreaOfEvaluationById))]
        public Task<List<AreaOfEvaluationModel>> GetAreaOfEvaluationById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<AreaOfEvaluationModel>($"Select * from [dbo].[tblManageAreaOfEvaluation] where ManageAreaOfEvalutionID={Id}", null,
            commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(AreaOfEvaluationModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ManageAreaOfEvalutionID", data.ManageAreaOfEvalutionID);
            dbparams.Add("@FocusArea", data.FocusArea);
            var updateAreaOfEvaluation = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateAreaOfEvaluation]",
               dbparams,
               commandType: CommandType.StoredProcedure));
            return updateAreaOfEvaluation;
        }
    }
}
