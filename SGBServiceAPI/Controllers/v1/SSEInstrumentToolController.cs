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

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SSEInstrumentToolController : ControllerBase
    {
        private readonly IDapper _dapper;

        public SSEInstrumentToolController(IDapper dapper)
        {
            _dapper = dapper;

        }

        [HttpPost(nameof(CreateInstrumentTool))]
        public async Task<int> CreateInstrumentTool(SSEInstrumentToolModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", data.Id, DbType.Int32);
            dbparams.Add("@SSEInstrumentName", data.SSEInstrumentName);
            dbparams.Add("@Year", data.Year );
            dbparams.Add("@Comments", data.Comment);
            dbparams.Add("@UserId", data.UserId);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spCreateSSEInstrumentTool]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id");
            return retVal;
        }

        [HttpPatch(nameof(UpdateInstrumentTool))]
        public Task<int> UpdateInstrumentTool(SSEInstrumentToolModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", data.Id, DbType.Int32);
            dbparams.Add("@SSEInstrumentName", data.SSEInstrumentName);
            dbparams.Add("@Year", data.Year);
            dbparams.Add("@KPIArrayId", data.KPIArrayId);
            dbparams.Add("@KPIJson", data.KPIJson);
            dbparams.Add("@SSEAuditTrail", data.SSEAuditTrail);
            dbparams.Add("@Status", data.Status);
            dbparams.Add("@Comment", data.Comment);
            dbparams.Add("@UserId", data.UserId);
            var updateKPI = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSSEInstrumentTool]",
               dbparams,
               commandType: CommandType.StoredProcedure));
            return updateKPI;
        }

        [HttpGet(nameof(GetInstrumentToolList))]
        public Task<List<SSEInstrumentToolModel>> GetInstrumentToolList()
        {
            var result = Task.FromResult(_dapper.GetAll<SSEInstrumentToolModel>($"Select tblinstrument.*,tblstatus.Status  from [dbo].[tblSSEInstrumentTool] tblinstrument INNER JOIN [dbo].[tblSSEStatus] tblstatus on tblstatus.SSEStatusID = tblinstrument.StatusID", null,
            commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetInstrumentToolKPIs))]
        public Task<List<SSEInstrumentToolKPIModel>> GetInstrumentToolKPIs()
        {

            var myQuery = "select  mc.ManageComponentID, sse.ManageAreaOfEvaluationID,sse.ManageAreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG( sse.ManageAreaOfEvaluationID, 1,0) OVER (ORDER BY sse.ManageAreaOfEvaluationID,sse.[ManageComponentID]) AS PreviousAreaOfEvaluation, sse.[ManageComponentID] AS CurrentComponent, LAG( sse.[ManageComponentID], 1,0) OVER (ORDER BY sse.ManageAreaOfEvaluationID, sse.[ManageComponentID]) AS PreviousComponent, " +
                "sse.[KPIName], mae.FocusArea, mc.ComponentName, sseIT.Year, sseIT.ManagementPlanID, ssStatus.Status, mmc.Compulsory, mr.Rating, kpi.ManageKPIID, kpi.BusinessUnit from [dbo].[tblSSEInstrumentList] sse " +
               "INNER JOIN[dbo].[tblManageAreaOfEvaluation] mae ON mae.ManageAreaOfEvalutionID = sse.ManageAreaOfEvaluationID " +
                "INNER JOIN[dbo].[tblManageComponents] mc ON mc.ManageComponentID = sse.ManageComponentID " +
                "INNER JOIN[dbo].[tblSSEInstrumentTool] sseIT ON sseIT.Id = sse.SSEInstrumentID " +
                "INNER JOIN[dbo].[tblManagementPlan] mp ON mp.PlanID = sseIT.ManagementPlanID " +
               " INNER JOIN[dbo].[tblSSEStatus] ssStatus ON ssStatus.SSEStatusID = sse.Status " +
                "INNER JOIN[dbo].[tblManageCompulsory] mmc ON mmc.CompulsoryID = sse.Compulsory " +
                "INNER JOIN[dbo].[tblManageRating] mr ON mr.RatingID = sse.Rating " +
               " INNER JOIN[dbo].[tblManageKPI] kpi ON kpi.KPIName = sse.KPIName " +
               " ORDER by sse.ManageAreaOfEvaluationID, sse.[ManageComponentID] ";





           var result = Task.FromResult(_dapper.GetAll<SSEInstrumentToolKPIModel>(myQuery, null,
            commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetSaveSSEByEmisNumber))]
        public Task<List<SSEInstrumentToolKPIModel>> GetSaveSSEByEmisNumber(int UserID, int EmisNumber)
        {

            var myQuery = "select  mc.ManageComponentID, sse.ManageAreaOfEvaluationID,sse.ManageAreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG( sse.ManageAreaOfEvaluationID, 1,0) OVER (ORDER BY sse.ManageAreaOfEvaluationID,sse.[ManageComponentID]) AS PreviousAreaOfEvaluation, " +
                "sse.[ManageComponentID] AS CurrentComponent, LAG( sse.[ManageComponentID], 1,0) OVER (ORDER BY sse.ManageAreaOfEvaluationID, sse.[ManageComponentID]) AS PreviousComponent, " +
                "sse.[KPIName], mae.FocusArea, mc.ComponentName, sseIT.Year, sseIT.ManagementPlanID, ssStatus.Status, mmc.Compulsory, mr.Rating, kpi.ManageKPIID from [dbo].[tblSSEInstrumentList] sse " +
                "INNER JOIN [dbo].[tblManageAreaOfEvaluation] mae ON mae.ManageAreaOfEvalutionID = sse.ManageAreaOfEvaluationID " +
                "INNER JOIN [dbo].[tblManageComponents] mc ON mc.ManageComponentID = sse.ManageComponentID " +
                "INNER JOIN [dbo].[tblSSEInstrumentTool] sseIT ON sseIT.Id = sse.SSEInstrumentID " +
                "INNER JOIN [dbo].[tblManagementPlan] mp ON mp.PlanID = sseIT.ManagementPlanID " +
                "INNER JOIN [dbo].[tblSSEStatus] ssStatus ON ssStatus.SSEStatusID = sse.Status " +
                "INNER JOIN [dbo].[tblManageCompulsory] mmc ON mmc.CompulsoryID = sse.Compulsory " +
                "INNER JOIN [dbo].[tblManageRating] mr ON mr.RatingID = sse.Rating " +
                "INNER JOIN [dbo].[tblManageKPI] kpi ON kpi.KPIName = sse.KPIName " +
                "ORDER by sse.ManageAreaOfEvaluationID, sse.[ManageComponentID]";



            var result = Task.FromResult(_dapper.GetAll<SSEInstrumentToolKPIModel>(myQuery, null,
             commandType: CommandType.Text));
            return result;
        }
    }
}
