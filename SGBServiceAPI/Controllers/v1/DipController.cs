using Dapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class DipController : ControllerBase
    {
        private readonly IDapper _dapper;
        public DipController(IDapper dapper)
        {
            _dapper = dapper;

        }
        [HttpPost(nameof(CreateDip))]
        public async Task<bool> CreateDip (DipModel createModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@IsPublishing", createModel.IsPublishing);
            dbparams.Add("@CompletedJSON", createModel.CompletedJSON);
            dbparams.Add("@EmisNumber", createModel.EmisNumber);
            dbparams.Add("@UserId", createModel.UserID);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_CreateOrUpdateSSESchoolLevel]"
            , dbparams,
            commandType: CommandType.StoredProcedure));

            var retVal = true;
            return retVal;
        }


        [HttpPost(nameof(CreateOrUpdateDip))]
        public async Task<bool> CreateOrUpdateDip(DipModel createModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@IsPublishing", createModel.IsPublishing);
            dbparams.Add("@CompletedJSON", createModel.CompletedJSON);
            dbparams.Add("@EmisNumber", createModel.EmisNumber);
            dbparams.Add("@UserId", createModel.UserID);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_DIPLoggedDistrictLevel]"
            , dbparams,
            commandType: CommandType.StoredProcedure));

            var retVal = true;
            return retVal;
        }

        [HttpPost(nameof(SendSSEToSchoolForEvidence))]
        public async Task<bool> SendSSEToSchoolForEvidence(DipModel schoolKPIModel)
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
        public async Task<bool> IsPriorityFromSchool(DipModel schoolKPIModel)
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

        [HttpGet(nameof(GetDipByDistrictID))]
        public Task<List<DipModel>> GetDipByDistrictID(int DisctrictID)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<DipModel>($"Select dip.*, maoe.FocusArea, com.ComponentName, kpi.KPIName from [dbo].[tblDipKPI] dip Inner join [dbo].[tblManageAreaOfEvaluation] maoe on maoe.ManageAreaOfEvalutionID = dip.AreaOfEvaluationID Inner Join [dbo].[tblManageComponents] com on com.ManageComponentID = dip.ComponentID Inner join [dbo].[tblManageKPI] kpi on kpi.ManageKPIID = dip.KPIID where dip.DistrictUserId = {DisctrictID}", null,
            commandType: CommandType.Text));


            return SchoolResponse;
        }


        [HttpGet(nameof(GetDipByDistrictIsDIPPrioritise))]
        public Task<List<DipModel>> GetDipByDistrictIsDIPPrioritise(int DisctrictID)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<DipModel>($"Select dip.*, maoe.FocusArea, com.ComponentName, kpi.KPIName from [dbo].[tblDipKPI] dip Inner join [dbo].[tblManageAreaOfEvaluation] maoe on maoe.ManageAreaOfEvalutionID = dip.AreaOfEvaluationID Inner Join [dbo].[tblManageComponents] com on com.ManageComponentID = dip.ComponentID Inner join [dbo].[tblManageKPI] kpi on kpi.ManageKPIID = dip.KPIID where dip.DistrictUserId = {DisctrictID}", null,
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

        [HttpGet(nameof(GetKPISchoolResponseAll))]
        public Task<List<DipModel>> GetKPISchoolResponseAll(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<DipModel>($"SELECT  AreaOfEvaluationID ManageAreaOfEvaluationID,  ComponentID ManageComponentID, AreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG(AreaOfEvaluationID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, ComponentID) AS PreviousAreaOfEvaluation,[ComponentID] AS CurrentComponent, LAG(ComponentID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, [ComponentID]) AS PreviousComponent, Response, IsSip, Status,[tblManageAreaOfEvaluation].FocusArea ,[tblManageComponents].ComponentName,[KPIName], Comment, Evidence EvidenceDescription, Documentpath,EmisNumber EmisNo,DateCaptured,UserId,KPIID FROM[dbo].[tblSchoolKPI] INNER JOIN dbo.[tblManageKPI]  ON[dbo].[tblManageKPI].ManageKPIID = [dbo].[tblSchoolKPI].KPIID Inner Join[dbo].[tblManageComponents] on[tblManageComponents].ManageComponentID = [tblSchoolKPI].ComponentID Inner Join[dbo].[tblManageAreaOfEvaluation] on[tblManageAreaOfEvaluation].ManageAreaOfEvalutionID = [tblSchoolKPI].AreaOfEvaluationID WHERE [tblSchoolKPI].EmisNumber = " + EmisNumber, null,commandType: CommandType.Text));

            return SchoolResponse;
        }

        [HttpGet(nameof(GetKPISchoolBySchool))]
        public Task<List<DipModel>> GetKPISchoolBySchool(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<DipModel>($"Select * from [dbo].[tblSchoolKPI] WHERE EmisNumber="+ EmisNumber, null,
            commandType: CommandType.Text));


            //foreach (SchoolKPIModel Schoolkpiitems in SchoolResponse.Result)
            //{

            //    var KPIComments = Task.FromResult(_dapper.GetAll<KPICommentsModel>($"Select * from [dbo].[tblKPIComments] where [SchoolKPIID] = {Schoolkpiitems.KPIID}", null,
            //       commandType: CommandType.Text));

            //    Schoolkpiitems.Comments = KPIComments.Result;



            //}
            return SchoolResponse;
        }


        [HttpGet(nameof(GetSIPByEmisNumber))]
        public Task<List<DipModel>> GetSIPByEmisNumber(int EmisNumber)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<DipModel>($"SELECT skpi.*, kpi.KPIName, maoe.FocusArea, com.ComponentName FROM tblSchoolKPI skpi Inner join [dbo].[tblManageKPI] kpi ON kpi.ManageKPIID = skpi.SchoolKPIID INNER JOIN [dbo].[tblManageAreaOfEvaluation] maoe ON maoe.ManageAreaOfEvalutionID = kpi.ManageAreaOfEvaluationID INNER JOIN [dbo].[tblManageComponents] com ON com.ManageComponentID = kpi.ManageComponentID WHERE Status='Approved' and IsSip=1 and EmisNumber = {EmisNumber}", null,
            commandType: CommandType.Text));

            return SchoolResponse;
        }

        
        [HttpGet(nameof(GetSchoolProfileByEmisNumber))]
        public Task<List<DipModel>> GetSchoolProfileByEmisNumber(int EmisNumber)
        {
            //var schoolProfiling = Task.FromResult(_dapper.GetAll<DipModel>($"SELECT [dbo].[tblAreaOfEvaluation].areaOfEvaluationName areaOfEvaluationName,SUM(CAST(Response as numeric(10,2))),SUM(CAST(Response as numeric(10,2))) * [dbo].[tblAreaOfEvaluation].Weight Score,(CASE WHEN (((SUM(CAST(Response as numeric(10,2)))*5)/175)*[dbo].[tblAreaOfEvaluation].Weight) >= 8 THEN 'GREEN' WHEN (((SUM(CAST(Response as numeric(10,2)))*5)/175)*10) < 8 THEN 'RED' END) RAG FROM tblSchoolKPI KPI INNER JOIN [dbo].[tblManageKPI] ON KPI.SchoolKPIID = [dbo].[tblManageKPI].ManageKPIID INNER JOIN [dbo].[tblAreaOfEvaluation] ON [dbo].[tblAreaOfEvaluation].areaOfEvaluationID = ManageAreaOfEvaluationID GROUP BY [dbo].[tblAreaOfEvaluation].areaOfEvaluationName,Weight", null,
            var schoolProfiling = Task.FromResult(_dapper.GetAll<DipModel>($"SELECT kpi.SchoolKPIID, kpi.Response, mae.Weight, mae.ManageAreaOfEvalutionID, mae.FocusArea, sipActionPlan.IsCompleted FROM [dbo].[tblSchoolKPI] kpi INNER JOIN [dbo].[tblSIPActionPlan] sipActionPlan ON sipActionPlan.SchoolKPIID = kpi.SchoolKPIID INNER JOIN [dbo].[tblManageAreaOfEvaluation] mae ON mae.ManageAreaOfEvalutionID = kpi.AreaOfEvaluationID WHERE kpi.Status = 'SIPPublish' and kpi.SchoolKPIID = 2549", null,
            commandType: CommandType.Text));

            return schoolProfiling;
        }



    }
}
