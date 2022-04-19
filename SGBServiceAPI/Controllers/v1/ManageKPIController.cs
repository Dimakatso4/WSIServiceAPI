using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WSIServiceAPI.Models;
using WSIServiceAPI.Services;

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageKPIController : ControllerBase
    {
        //private IConfiguration _config;
        private readonly IDapper _dapper;

        public ManageKPIController(IDapper dapper)
        {
            //_config = config;
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateKPI))]
        public async Task<int> CreateKPI(ManageKPIModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@ManageAreaOfEvaluationID", data.ManageAreaOfEvaluationID, DbType.Int32);
            dataBaseParams.Add("@ManageComponentID", data.ManageComponentID, DbType.Int32);
            dataBaseParams.Add("@KPIName", data.KPIName, DbType.String);
            dataBaseParams.Add("@CompulsoryID", data.CompulsoryID, DbType.Int32);
            dataBaseParams.Add("@RatingID", data.RatingId, DbType.Int32);
            dataBaseParams.Add("@BusinessUnitID", data.BusinessUnitID, DbType.Int32);
            dataBaseParams.Add("@LegislationID", data.LegislationID, DbType.Int32);
            dataBaseParams.Add("@Resources", data.Resources, DbType.String);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddKPIQuestions]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = Output;
            return returnVal;
        }

        [HttpGet(nameof(GetAllKPIQuestion))]
        public Task<List<ManageKPIModel>> GetAllKPIQuestion()
        {
            /*var Output = Task.FromResult(_dapper.GetAll<SSEComponentModel>($"select * from [dbo].[tblAreaOfEvaluation]", null,
                    commandType: CommandType.Text));*/

            return Task.FromResult(_dapper.GetAll<ManageKPIModel>($"Select kpi.*, mav.FocusArea, mc.ComponentId, sse.ManageAreaOfEvaluationID, com.ComponentName, mr.Description as 'Rating'   from [dbo].[tblManageKPI] kpi Inner Join [dbo].[tblManageAreaOfEvaluation] mav on mav.ManageAreaOfEvalutionID = kpi.ManageAreaOfEvaluationID Inner Join [dbo].[tblManageComponents] com on com.ManageComponentID = kpi.ManageComponentID Inner Join [dbo].[tblManageRating] mr on mr.RatingID = kpi.Rating order by ManageAreaOfEvaluationID, ManageComponentID, KPIName", null,
                    commandType: CommandType.Text));

        }

        [HttpGet(nameof(GetKPIQuestionByID))]
        public Task<List<ManageKPIModel>> GetKPIQuestionByID(int KPIID)
        {
            var ComponentList = Task.FromResult(_dapper.GetAll<ManageKPIModel>($"Select kpi.*, mav.FocusArea, com.ComponentName, leg.Description as 'Legislation', mr.Description as 'Rating', bu.UnitName   from [dbo].[tblManageKPI] kpi Inner Join [dbo].[tblManageAreaOfEvaluation] mav on mav.ManageAreaOfEvalutionID = kpi.ManageAreaOfEvaluationID Inner Join [dbo].[tblManageComponents] com on com.ManageComponentID = kpi.ManageComponentID Inner Join [dbo].[tblManageLegislation] leg on leg.LegislationID = kpi.Legislation Inner Join [dbo].[tblManageRating] mr on mr.RatingID = kpi.Rating Inner Join [dbo].[tblBusinessUnit] bu on bu.UnitId = kpi.BusinessUnit where kpi.ManageKPIID = {KPIID}", null,
                    commandType: CommandType.Text));
            return ComponentList;
        }

        [HttpGet(nameof(GetKPIQuestionByAreaOfEvaluationID))]
        public Task<List<ManageKPIModel>> GetKPIQuestionByAreaOfEvaluationID(int areaOfEvaluationID)
        {
            var ComponentList = Task.FromResult(_dapper.GetAll<ManageKPIModel>($"Select kpi.*, mav.FocusArea, mc.ComponentId, sse.ManageAreaOfEvaluationID, com.ComponentName, leg.Description as 'Legislation', mr.Description as 'Rating', bu.UnitName   from [dbo].[tblManageKPI] kpi Inner Join [dbo].[tblManageAreaOfEvaluation] mav on mav.ManageAreaOfEvalutionID = kpi.ManageAreaOfEvaluationID Inner Join [dbo].[tblManageComponents] com on com.ManageComponentID = kpi.ManageComponentID Inner Join [dbo].[tblManageLegislation] leg on leg.LegislationID = kpi.Legislation Inner Join [dbo].[tblManageRating] mr on mr.RatingID = kpi.Rating Inner Join [dbo].[tblBusinessUnit] bu on bu.UnitId = kpi.BusinessUnit where mav.ManageAreaOfEvalutionID = {areaOfEvaluationID}", null,
                    commandType: CommandType.Text));
            return ComponentList;
        }

        [HttpPatch(nameof(UpdateKPI))]
        public Task<int> UpdateKPI(ManageKPIModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@ManageKPIID", data.ManageKPIID, DbType.Int32);
            dataBaseParams.Add("@ManageAreaOfEvaluationID", data.ManageAreaOfEvaluationID, DbType.Int32);
            dataBaseParams.Add("@ManageComponentID", data.ManageComponentID, DbType.Int32);
            dataBaseParams.Add("@KPIName", data.KPIName, DbType.String);
            dataBaseParams.Add("@CompulsoryID", data.CompulsoryID, DbType.Int32);
            dataBaseParams.Add("@RatingID", data.RatingId, DbType.Int32);
            dataBaseParams.Add("@BusinessUnitID", data.BusinessUnitID, DbType.Int32);
            dataBaseParams.Add("@LegislationID", data.LegislationID, DbType.Int32);
            dataBaseParams.Add("@Resources", data.Resources, DbType.String);

            var updateKPI = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateKPIQuestion]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateKPI;
        }
    }
}
