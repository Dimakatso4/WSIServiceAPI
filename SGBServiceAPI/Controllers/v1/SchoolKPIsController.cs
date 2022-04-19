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
    public class SchoolKPIsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SchoolKPIsController(IDapper dapper)
        {
            _dapper = dapper;

        }

        [HttpPost(nameof(CreateResponse))]
        public async Task<bool> CreateResponse (CreateSSEModel createSSEModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@IsPublishing", createSSEModel.IsPublishing);
            dbparams.Add("@CompletedJSON", createSSEModel.CompletedJSON);
            dbparams.Add("@EmisNumber", createSSEModel.EmisNumber);
            dbparams.Add("@UserId", createSSEModel.UserId);

            //var result_Old = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_CreateOrUpdateSSESchoolLevel]"

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_CreateOrUpdateSSESchoolLevel]"

            , dbparams,
            commandType: CommandType.StoredProcedure));

            var retVal = true;
            return retVal;
        }


        [HttpPost(nameof(SaveSSE))]
        public async Task<bool> SaveSSE(CreateSSEModel createSSEModel)
        {
            var dbparams = new DynamicParameters();

            
            dbparams.Add("@CompletedJSON", createSSEModel.CompletedJSON);
            dbparams.Add("@EmisNumber", createSSEModel.EmisNumber);
            dbparams.Add("@UserId", createSSEModel.UserId);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_CreateOrUpdateSSESchoolLevelNoStatus]"

            , dbparams,
            commandType: CommandType.StoredProcedure));

            var retVal = true;
            return retVal;
        }


        [HttpPost(nameof(PublishSSE))]
        public async Task<bool> PublishSSE(CreateSSEModel createSSEModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@IsPublishing", createSSEModel.IsPublishing);
            dbparams.Add("@CompletedJSON", createSSEModel.CompletedJSON);
            dbparams.Add("@EmisNumber", createSSEModel.EmisNumber);
            dbparams.Add("@UserId", createSSEModel.UserId);

            //var result_Old = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_CreateOrUpdateSSESchoolLevel]"

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_PublishSSESchoolLevel]"

            , dbparams,
            commandType: CommandType.StoredProcedure));

            var retVal = true;
            return retVal;
        }


        [HttpPost(nameof(SIPLogged))]
        public async Task<bool> SIPLogged(CreateSSEModel createSSEModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@CompletedJSON", createSSEModel.CompletedJSON);
            dbparams.Add("@EmisNumber", createSSEModel.EmisNumber);
            dbparams.Add("@UserId", createSSEModel.UserId);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_SIPLoggedSchoolLevel]"

            , dbparams,
            commandType: CommandType.StoredProcedure));

            var retVal = true;
            return retVal;
        }


        [HttpPost(nameof(UpdateSIP))]
        public async Task<bool> UpdateSIP(CreateSSEModel createSSEModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@CompletedJSON", createSSEModel.CompletedJSON);
            dbparams.Add("@EmisNumber", createSSEModel.EmisNumber);
            dbparams.Add("@UserId", createSSEModel.UserId);
            dbparams.Add("@Status", createSSEModel.Status);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_UpdateSIPSchoolLevel]"
            , dbparams,
            commandType: CommandType.StoredProcedure));

            var retVal = true;
            return retVal;
        }

        [HttpPost(nameof(SendSSEToSchoolForEvidence))]
        public async Task<bool> SendSSEToSchoolForEvidence(SchoolKPIModel schoolKPIModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@EmisNumber", schoolKPIModel.EmisNumber);
            dbparams.Add("@UserID", schoolKPIModel.UserID);
            dbparams.Add("@IDs", schoolKPIModel.IDs);

            var result = await Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateSSEItemsForEvidence]"
            , dbparams,
            commandType: CommandType.StoredProcedure));

            var retVal = true;
            return retVal;
        }

        [HttpPost(nameof(IsPriorityFromSchool))]
        public async Task<bool> IsPriorityFromSchool(SchoolKPIModel schoolKPIModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@EmisNumber", schoolKPIModel.EmisNumber);
            dbparams.Add("@UserID", schoolKPIModel.UserID);
            dbparams.Add("@IDs", schoolKPIModel.IDs);

            var result = await Task.FromResult(_dapper.Update<int>("[dbo].[Sp_IsPriorityFromSchool]"
            , dbparams,
            commandType: CommandType.StoredProcedure));

            var retVal = true;
            return retVal;
        }

        [HttpGet(nameof(GetKPISchoolResponseBySchoolID))]
        public Task<List<SchoolKPIModel>> GetKPISchoolResponseBySchoolID(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"Select * from [dbo].[tblSchoolKPI] where EmisNumber = {EmisNumber} and Response <= 2 and Status = Published", null,
            commandType: CommandType.Text));


            return SchoolResponse;
        }

        //[HttpGet(nameof(GetKPISchoolResponseAll))]
        //public Task<List<SchoolKPIModel>> GetKPISchoolResponseAll()
        //{
        //    var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"Select * from [dbo].[tblSchoolKPI]", null,
        //    commandType: CommandType.Text));


        //    foreach (SchoolKPIModel Schoolkpiitems in SchoolResponse.Result)
        //    {

        //        var KPIComments = Task.FromResult(_dapper.GetAll<KPICommentsModel>($"Select * from [dbo].[tblKPIComments] where [SchoolKPIID] = {Schoolkpiitems.KPIID}", null,
        //           commandType: CommandType.Text));

        //        Schoolkpiitems.Comments = KPIComments.Result;



        //    }
        //    return SchoolResponse;
        //}



        [HttpGet(nameof(CheckIfSSEExists))]
        public Task<int> CheckIfSSEExists(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.Get<int>($"SELECT COUNT(kpiid) FROM [dbo].[tblSchoolKPI]  WHERE EmisNumber = " + EmisNumber, null, commandType: CommandType.Text));

            return SchoolResponse;
        }

        [HttpGet(nameof(GetSIPSchoolLevel))]
        public Task<List<SchoolKPIModel>> GetSIPSchoolLevel(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT  SchoolKPIID Id,AreaOfEvaluationID ManageAreaOfEvaluationID,  ComponentID ManageComponentID, AreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG(AreaOfEvaluationID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, ComponentID) AS PreviousAreaOfEvaluation,[ComponentID] AS CurrentComponent, LAG(ComponentID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, [ComponentID]) AS PreviousComponent, Response, IsSip, IsPrioritise, Status,[tblManageAreaOfEvaluation].FocusArea ,[tblManageComponents].ComponentName,[KPIName], Comment, Evidence EvidenceDescription, Documentpath,EmisNumber EmisNo,DateCaptured,UserId,KPIID, schoolKPIID,PreviousRating FROM[dbo].[tblSchoolKPI] INNER JOIN dbo.[tblManageKPI]  ON[dbo].[tblManageKPI].ManageKPIID = [dbo].[tblSchoolKPI].KPIID Inner Join[dbo].[tblManageComponents] on[tblManageComponents].ManageComponentID = [tblSchoolKPI].ComponentID Inner Join[dbo].[tblManageAreaOfEvaluation] on[tblManageAreaOfEvaluation].ManageAreaOfEvalutionID = [tblSchoolKPI].AreaOfEvaluationID WHERE IsSip=1 AND Status='SSEPublished' AND [tblSchoolKPI].EmisNumber = " + EmisNumber + " Order By AreaOfEvaluationID", null,commandType: CommandType.Text));

            return SchoolResponse;
        }

        [HttpGet(nameof(GetSIPPublished))]
        public Task<List<SchoolKPIModel>> GetSIPPublished(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT  SchoolKPIID Id,AreaOfEvaluationID ManageAreaOfEvaluationID,  ComponentID ManageComponentID, AreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG(AreaOfEvaluationID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, ComponentID) AS PreviousAreaOfEvaluation,[ComponentID] AS CurrentComponent, LAG(ComponentID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, [ComponentID]) AS PreviousComponent, Response, IsSip, IsPrioritise, Status,[tblManageAreaOfEvaluation].FocusArea ,[tblManageComponents].ComponentName,[KPIName], Comment, Evidence EvidenceDescription, Documentpath,EmisNumber EmisNo,DateCaptured,UserId,KPIID,schoolKPIID,PreviousRating FROM[dbo].[tblSchoolKPI] INNER JOIN dbo.[tblManageKPI]  ON[dbo].[tblManageKPI].ManageKPIID = [dbo].[tblSchoolKPI].KPIID Inner Join[dbo].[tblManageComponents] on[tblManageComponents].ManageComponentID = [tblSchoolKPI].ComponentID Inner Join[dbo].[tblManageAreaOfEvaluation] on[tblManageAreaOfEvaluation].ManageAreaOfEvalutionID = [tblSchoolKPI].AreaOfEvaluationID WHERE IsSip=1 AND Status='SIPPublished' AND [tblSchoolKPI].EmisNumber = " + EmisNumber + " Order By AreaOfEvaluationID", null, commandType: CommandType.Text));

            return SchoolResponse;
        }

        [HttpGet(nameof(GetSIPLogged))]
        public Task<List<SchoolKPIModel>> GetSIPLogged(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT  SchoolKPIID Id,AreaOfEvaluationID ManageAreaOfEvaluationID,  ComponentID ManageComponentID, AreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG(AreaOfEvaluationID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, ComponentID) AS PreviousAreaOfEvaluation,[ComponentID] AS CurrentComponent, LAG(ComponentID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, [ComponentID]) AS PreviousComponent, Response, IsSip,SIPClusterComments, IsPrioritise, Status,[tblManageAreaOfEvaluation].FocusArea ,[tblManageComponents].ComponentName,[KPIName], Comment, Evidence EvidenceDescription, Documentpath,EmisNumber EmisNo,DateCaptured,UserId,KPIID schoolKPIID,PreviousRating FROM[dbo].[tblSchoolKPI] INNER JOIN dbo.[tblManageKPI]  ON[dbo].[tblManageKPI].ManageKPIID = [dbo].[tblSchoolKPI].KPIID Inner Join[dbo].[tblManageComponents] on[tblManageComponents].ManageComponentID = [tblSchoolKPI].ComponentID Inner Join[dbo].[tblManageAreaOfEvaluation] on[tblManageAreaOfEvaluation].ManageAreaOfEvalutionID = [tblSchoolKPI].AreaOfEvaluationID WHERE IsSip=1 AND (Status='SIPLogged' OR Comment IS NOT NULL)  AND IsPrioritise=1 AND [tblSchoolKPI].EmisNumber = " + EmisNumber + " Order By AreaOfEvaluationID", null, commandType: CommandType.Text));

            return SchoolResponse;
        }


        [HttpGet(nameof(GetDistrictSIP))]
        public Task<List<SchoolKPIModel>> GetDistrictSIP(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT  SchoolKPIID Id,AreaOfEvaluationID ManageAreaOfEvaluationID,  ComponentID ManageComponentID, AreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG(AreaOfEvaluationID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, ComponentID) AS PreviousAreaOfEvaluation,[ComponentID] AS CurrentComponent, LAG(ComponentID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, [ComponentID]) AS PreviousComponent, Response,SIPClusterComments, IsSip, IsPrioritise, Status,[tblManageAreaOfEvaluation].FocusArea ,[tblManageComponents].ComponentName,[KPIName], Comment, Evidence EvidenceDescription, Documentpath,EmisNumber EmisNo,DateCaptured,UserId,KPIID schoolKPIID,PreviousRating FROM[dbo].[tblSchoolKPI] INNER JOIN dbo.[tblManageKPI]  ON[dbo].[tblManageKPI].ManageKPIID = [dbo].[tblSchoolKPI].KPIID Inner Join[dbo].[tblManageComponents] on[tblManageComponents].ManageComponentID = [tblSchoolKPI].ComponentID Inner Join[dbo].[tblManageAreaOfEvaluation] on[tblManageAreaOfEvaluation].ManageAreaOfEvalutionID = [tblSchoolKPI].AreaOfEvaluationID WHERE IsSip=1 AND (Status IN ('SendForEvidence','SIPLogged','SIPPublished') OR Comment IS NOT NULL)  AND [tblSchoolKPI].EmisNumber = " + EmisNumber + " Order By AreaOfEvaluationID", null, commandType: CommandType.Text));

            return SchoolResponse;
        }


        [HttpGet(nameof(GetSSEPublished))]
        public Task<List<SchoolKPIModel>> GetSSEPublished(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT  SchoolKPIID Id,AreaOfEvaluationID ManageAreaOfEvaluationID,  ComponentID ManageComponentID, AreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG(AreaOfEvaluationID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, ComponentID) AS PreviousAreaOfEvaluation,[ComponentID] AS CurrentComponent, LAG(ComponentID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, [ComponentID]) AS PreviousComponent, Response, IsSip,SIPClusterComments, IsPrioritise, Status,[tblManageAreaOfEvaluation].FocusArea ,[tblManageComponents].ComponentName,[KPIName], Comment, Evidence EvidenceDescription, Documentpath,EmisNumber EmisNo,DateCaptured,UserId,KPIID schoolKPIID,PreviousRating FROM[dbo].[tblSchoolKPI] INNER JOIN dbo.[tblManageKPI]  ON[dbo].[tblManageKPI].ManageKPIID = [dbo].[tblSchoolKPI].KPIID Inner Join[dbo].[tblManageComponents] on[tblManageComponents].ManageComponentID = [tblSchoolKPI].ComponentID Inner Join[dbo].[tblManageAreaOfEvaluation] on[tblManageAreaOfEvaluation].ManageAreaOfEvalutionID = [tblSchoolKPI].AreaOfEvaluationID WHERE IsSip=1 AND Status IN ('SSEPublished','SIPLogged','SIPPublished','SendForEvidence') AND [tblSchoolKPI].EmisNumber = " + EmisNumber + " Order By AreaOfEvaluationID", null, commandType: CommandType.Text));

            return SchoolResponse;
        }

        [HttpGet(nameof(GetKPISchoolResponseAll))]
        public Task<List<SchoolKPIModel>> GetKPISchoolResponseAll(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT skpi.ComponentID, skpi.AreaOfEvaluationID,skpi.AreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG( skpi.AreaOfEvaluationID, 1,0) OVER (ORDER BY skpi.AreaOfEvaluationID,skpi.ComponentID) AS PreviousAreaOfEvaluation, skpi.ComponentID AS CurrentComponent, LAG( skpi.ComponentID, 1,0) OVER (ORDER BY skpi.AreaOfEvaluationID, skpi.ComponentID) AS PreviousComponent, skpi.*, kpi.KPIName,skpi.Evidence EvidenceDescription, maoe.FocusArea, com.ComponentName, rating.Rating, Response, comments.Comment, SchoolComment,isSip,PreviousRating FROM tblSchoolKPI skpi Inner join [dbo].[tblManageKPI] kpi ON kpi.ManageKPIID = skpi.KPIID INNER JOIN [dbo].[tblManageAreaOfEvaluation] maoe ON maoe.ManageAreaOfEvalutionID = kpi.ManageAreaOfEvaluationID INNER JOIN [dbo].[tblManageRating] rating ON rating.RatingID = kpi.RatingID LEFT JOIN [dbo].[tblKPIComments] comments ON comments.SchoolKPIID = kpi.ManageKPIID INNER JOIN [dbo].[tblManageComponents] com ON com.ManageComponentID = kpi.ManageComponentID WHERE (Status='SSESendForEvidence' or Status='SSELogged' or Status='SSEPublished' or Status='Created' or status='SSEApproved' or status='SSEBUApproved' or status='SSESendForBUComment' or status='SSESendForCircuitComment' or status='sipBUComment' or status='sipSchoolComment' or status='sipDirectorComment') and EmisNumber =" + EmisNumber, null,commandType: CommandType.Text));
            return SchoolResponse;
        }

        [HttpGet(nameof(GetKPISchoolResponseTotals))]
        public Task<List<SchoolKPIModel>> GetKPISchoolResponseTotals(int AreaOfEvaluationID, int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT skpi.AreaOfEvaluationID, count(*) as 'TotalNumber', SUM(CASE WHEN skpi.Response > 0 OR skpi.Response = 'Yes' OR skpi.Response = 'No' OR skpi.Response = 'N/A' THEN 1 ELSE 0 END) AS CompletedNumber FROM tblSchoolKPI skpi Inner join [dbo].[tblManageKPI] kpi ON kpi.ManageKPIID = skpi.KPIID INNER JOIN [dbo].[tblManageAreaOfEvaluation] maoe ON maoe.ManageAreaOfEvalutionID = kpi.ManageAreaOfEvaluationID INNER JOIN [dbo].[tblManageRating] rating ON rating.RatingID = kpi.RatingID LEFT JOIN [dbo].[tblKPIComments] comments ON comments.SchoolKPIID = kpi.ManageKPIID INNER JOIN [dbo].[tblManageComponents] com ON com.ManageComponentID = kpi.ManageComponentID WHERE EmisNumber = {EmisNumber} and skpi.AreaOfEvaluationID = {AreaOfEvaluationID} GROUP BY skpi.AreaOfEvaluationID", null, commandType: CommandType.Text));
            return SchoolResponse;
        }


        [HttpGet(nameof(GetKPISchoolBySchool))]
        public Task<List<SchoolKPIModel>> GetKPISchoolBySchool(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"Select * from [dbo].[tblSchoolKPI] WHERE EmisNumber="+ EmisNumber, null,
            commandType: CommandType.Text));


            return SchoolResponse;
        }


        [HttpGet(nameof(GetSIPByEmisNumber))]
        public Task<List<SchoolKPIModel>> GetSIPByEmisNumber(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT skpi.*, kpi.KPIName, maoe.FocusArea, com.ComponentName FROM tblSchoolKPI skpi Inner join [dbo].[tblManageKPI] kpi ON kpi.ManageKPIID = skpi.SchoolKPIID INNER JOIN [dbo].[tblManageAreaOfEvaluation] maoe ON maoe.ManageAreaOfEvalutionID = kpi.ManageAreaOfEvaluationID INNER JOIN [dbo].[tblManageComponents] com ON com.ManageComponentID = kpi.ManageComponentID WHERE Status='Approved' and IsSip=1 and EmisNumber = {EmisNumber}", null,
            commandType: CommandType.Text));

            return SchoolResponse;
        }

        
        [HttpGet(nameof(GetSchoolProfileByEmisNumber))]
        public Task<List<SchoolProfilingModel>> GetSchoolProfileByEmisNumber(int EmisNumber)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@EmisNumber", EmisNumber);

            var schoolProfiling = Task.FromResult(_dapper.GetAll<SchoolProfilingModel>($"[dbo].[spSchoolProfilingByAll]", dataBaseParams,
            commandType: CommandType.StoredProcedure));

            return schoolProfiling;
   
        }

        [HttpGet(nameof(GetSchoolProfileByEmisNumberQ2))]
        public Task<List<SchoolProfilingModel>> GetSchoolProfileByEmisNumberQ2(int EmisNumber)
        {
            var schoolProfiling = Task.FromResult(_dapper.GetAll<SchoolProfilingModel>($"Select distinct mae.ManageAreaOfEvalutionID, mae.FocusArea, Sum(Cast(schoolKPI.TotalRatingQuarter2 as decimal(10,2))) as 'PreviousRating2', Cast(Sum(Cast(schoolKPI.TotalRatingQuarter2 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) as 'Results2', (Case WHEN Cast(Sum(Cast(schoolKPI.TotalRatingQuarter2 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) <= 4 then 'Red' WHEN Cast(Sum(Cast(schoolKPI.TotalRatingQuarter2 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) > 4 and Cast(Sum(Cast(schoolKPI.TotalRatingQuarter2 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) < 8 then 'Amber' WHEN Cast(Sum(Cast(schoolKPI.TotalRatingQuarter2 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) >= 8 then 'Green' END ) as RAG from [dbo].[tblSchoolKPI] schoolKPI INNER JOIN [dbo].[tblManageAreaOfEvaluation] mae ON mae.ManageAreaOfEvalutionID = schoolKPI.AreaOfEvaluationID where [EmisNumber] = {EmisNumber} and Response <> 'N/A' Group by mae.ManageAreaOfEvalutionID, mae.FocusArea, mae.Weight", null,
            commandType: CommandType.Text));

            return schoolProfiling;
        }

        [HttpGet(nameof(GetSchoolProfileByEmisNumberQ3))]
        public Task<List<SchoolProfilingModel>> GetSchoolProfileByEmisNumberQ3(int EmisNumber)
        {
            var schoolProfiling = Task.FromResult(_dapper.GetAll<SchoolProfilingModel>($"Select distinct mae.ManageAreaOfEvalutionID, mae.FocusArea, Sum(Cast(schoolKPI.TotalRatingQuarter3 as decimal(10,2))) as 'PreviousRating3', Cast(Sum(Cast(schoolKPI.TotalRatingQuarter3 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) as 'Results3', (Case WHEN Cast(Sum(Cast(schoolKPI.TotalRatingQuarter3 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) <= 4 then 'Red' WHEN Cast(Sum(Cast(schoolKPI.TotalRatingQuarter3 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) > 4 and Cast(Sum(Cast(schoolKPI.TotalRatingQuarter3 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) < 8 then 'Amber' WHEN Cast(Sum(Cast(schoolKPI.TotalRatingQuarter3 as decimal(10,2))) / (Count(schoolKPI.KPIID) * 5) * 100 / mae.Weight as decimal(10,2)) >= 8 then 'Green' END ) as RAG from [dbo].[tblSchoolKPI] schoolKPI INNER JOIN [dbo].[tblManageAreaOfEvaluation] mae ON mae.ManageAreaOfEvalutionID = schoolKPI.AreaOfEvaluationID where [EmisNumber] = {EmisNumber} and Response <> 'N/A' Group by mae.ManageAreaOfEvalutionID, mae.FocusArea, mae.Weight", null,
            commandType: CommandType.Text));

            return schoolProfiling;
        }

        [HttpGet(nameof(GetSchoolProfileTotalScoreByEmisNumber))]
        public Task<SchoolProfilingModel> GetSchoolProfileTotalScoreByEmisNumber(int EmisNumber)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@EmisNumber", EmisNumber);

            var schoolProfiling = Task.FromResult(_dapper.Get<SchoolProfilingModel>($"spSchoolProfilingTotalResults" , dataBaseParams,
            commandType: CommandType.StoredProcedure));

            return schoolProfiling;
        }


    }
}
