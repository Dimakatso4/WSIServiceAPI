
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
    public class SIPActionPlanController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SIPActionPlanController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateSIPActionPlan))]
        public async Task<int> CreateSIPActionPlan(SIPActionPlanModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SIPId", data.SIPActionId, DbType.Int32);
            dbparams.Add("@AreaOfDevelopment", data.AreaOfDevelopment);
            dbparams.Add("@DescriptionOfActivities", data.DescriptionOfActivities);
            dbparams.Add("@TargetGroup", data.TargetGroup);
            dbparams.Add("@Responsibility", data.Responsibility);
            dbparams.Add("@StartDate", data.StartDate);
            dbparams.Add("@FinishDate", data.FinishDate);
            dbparams.Add("@Resources", data.Resources);
            dbparams.Add("@ProgressPerQuarter", data.ProgressPerQuarter);
            dbparams.Add("@KPIID", data.KPIID);
            dbparams.Add("@EmisNumber", data.EmisNumber);
            dbparams.Add("@Comment", data.Comment);
            dbparams.Add("@Status", data.Status);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddSIPActionPlan]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@SIPId");
            return retVal;
        }

        [HttpPost(nameof(CreateDIPActionPlan))]
        public async Task<int> CreateDIPActionPlan(SIPActionPlanModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SIPId", data.SIPActionId, DbType.Int32);
            dbparams.Add("@AreaOfDevelopment", data.AreaOfDevelopment);
            dbparams.Add("@DescriptionOfActivities", data.DescriptionOfActivities);
            dbparams.Add("@TargetGroup", data.TargetGroup);
            dbparams.Add("@Responsibility", data.Responsibility);
            dbparams.Add("@StartDate", data.StartDate);
            dbparams.Add("@FinishDate", data.FinishDate);
            dbparams.Add("@Resources", data.Resources);
            dbparams.Add("@ProgressPerQuarter", data.ProgressPerQuarter);
            dbparams.Add("@KPIID", data.KPIID);
            dbparams.Add("@Comment", data.Comment);
            dbparams.Add("@Status", data.Status);
            dbparams.Add("@DistrictCode", data.DistrictCode);
            dbparams.Add("@DIPBUComment", data.DIPBUComment);
            dbparams.Add("@DIPCircuitComent", data.DIPCircuitComent);
            dbparams.Add("@DIPDirectComment", data.DIPDirectComment);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddDIPActionPlan]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@SIPId");
            return retVal;
        }

        [HttpPost(nameof(CreatePIPActionPlan))]
        public async Task<int> CreatePIPActionPlan(SIPActionPlanModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SIPId", data.SIPActionId, DbType.Int32);
            dbparams.Add("@AreaOfDevelopment", data.AreaOfDevelopment);
            dbparams.Add("@DescriptionOfActivities", data.DescriptionOfActivities);
            dbparams.Add("@TargetGroup", data.TargetGroup);
            dbparams.Add("@Responsibility", data.Responsibility);
            dbparams.Add("@StartDate", data.StartDate);
            dbparams.Add("@FinishDate", data.FinishDate);
            dbparams.Add("@Resources", data.Resources);
            dbparams.Add("@ProgressPerQuarter", data.ProgressPerQuarter);
            dbparams.Add("@KPIID", data.KPIID);
            dbparams.Add("@Comment", data.Comment);
            dbparams.Add("@Status", data.Status);
            dbparams.Add("@HeadOffice", "HO");
            dbparams.Add("@PIPBuComment", data.PIPBuComment);
            dbparams.Add("@PIPLineDirectorComment", data.PIPLineDirectorComment);
            dbparams.Add("@PIPChiefComment", data.PIPChiefComment);
            dbparams.Add("@PIPDepComment", data.PIPDepComment);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddPIPActionPlan]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@SIPId");
            return retVal;
        }


        [HttpGet(nameof(GetSIPActionPlan))]
        public Task<List<SIPActionPlanModel>> GetSIPActionPlan()
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [tblSIPActionPlan]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetSIPActionPlanById))]
        public Task<List<SIPActionPlanModel>> GetSIPActionPlanById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [tblSIPActionPlan] where SIPId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }


        [HttpGet(nameof(GetSIPActionPlanByEmisNumber))]
        public Task<List<SIPActionPlanModel>> GetSIPActionPlanByEmisNumber(int EmisNumber, int KpiID)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [tblSIPActionPlan] where KPIID = {KpiID} and [EmisNumber] = {EmisNumber} order by  [FinishDate]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetDIPActionPlanByEmisNumberAllPublished))]
        public Task<List<SIPActionPlanModel>> GetDIPActionPlanByEmisNumberAllPublished(string DistrictCode, int kPIID)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [dbo].[tblSIPActionPlan] where [DistrictCode] = DistrictCode and [KPIID] = {kPIID} and [Status] = 'Published' order by  [StartDate] desc", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetDIPActionPlanForAddDIP))]
        public Task<List<SIPActionPlanModel>> GetDIPActionPlanForAddDIP(string DistrictCode, int kPIID)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [dbo].[tblSIPActionPlan] where [DistrictCode] = '{DistrictCode}' and [KPIID] = {kPIID} and [Status] = 'Published' OR [Status] = 'Change Request' OR [Status] = 'Action Approved'  OR [Status] = 'DIP Logged'  order by  [StartDate] desc", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetDIPActionPlanForVerification))]
        public Task<List<SIPActionPlanModel>> GetDIPActionPlanForVerification(string DistrictCode, int kPIID)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [dbo].[tblSIPActionPlan] where [DistrictCode] = '{DistrictCode}' and [KPIID] = {kPIID} and [Status] = 'Pending Review' OR [Status] = 'Partial Approved' OR [Status] = 'Published' OR [Status] = 'Update Request'  order by  [StartDate] desc", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetDIPActionPlanForReview))]
        public Task<List<SIPActionPlanModel>> GetDIPActionPlanForReview(string DistrictCode, int kPIID)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [dbo].[tblSIPActionPlan] where [DistrictCode] = '{DistrictCode}' and [KPIID] = {kPIID} and [Status] = 'Partial Approved' OR [Status] = 'Approved' OR [Status] = 'Request Update'  OR [Status] = 'Published'  order by  [StartDate] desc", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetSIPActionPlanByDistrictCode))]
        public Task<List<SIPActionPlanModel>> GetSIPActionPlanByDistrictCode(string DistrictCode,int kPIID)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [tblSIPActionPlan]  where [DistrictCode] = '{DistrictCode}' AND KPIID={kPIID} order by  [StartDate] desc", null,
                    commandType: CommandType.Text));
            return result;
        }



        [HttpGet(nameof(GetSIPActionPlanForHO))]
        public Task<List<SIPActionPlanModel>> GetSIPActionPlanForHO(int KpiID,string HO)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [tblSIPActionPlan] where KPIID = {KpiID} and [HeadOffice] = '{HO}' order by  [StartDate] desc", null,
                    commandType: CommandType.Text));
            return result;
        }


        [HttpGet(nameof(GetSIPActionPlanByEmisNumberDistrictLevel))]
        public Task<List<SIPActionPlanModel>> GetSIPActionPlanByEmisNumberDistrictLevel(int EmisNumber)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [tblSIPActionPlan] where [EmisNumber] = {EmisNumber} order by  FinishDate", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetSchoolById))]
        public Task<List<SIPActionPlanModel>> GetSchoolById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModel>($"select * from [tblSIPActionPlan] where SchoolId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }



        [HttpPost(nameof(UpdateSIPActionPlanPerKPI))]
        public Task<int> UpdateSIPActionPlanPerKPI(SIPActionPlanModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SIPId", data.SIPActionId, DbType.Int32,ParameterDirection.InputOutput);
            dbparams.Add("@AreaOfDevelopment", data.AreaOfDevelopment);
            dbparams.Add("@DescriptionOfActivities", data.DescriptionOfActivities);
            dbparams.Add("@TargetGroup", data.TargetGroup);
            dbparams.Add("@Responsibility", data.Responsibility);
            dbparams.Add("@StartDate", data.StartDate);
            dbparams.Add("@FinishDate", data.FinishDate);
            dbparams.Add("@Resources", data.Resources);
            dbparams.Add("@Comment", data.Comment);
            dbparams.Add("@Status", data.Status);
            dbparams.Add("@EmisNumber", data.EmisNumber);
            dbparams.Add("@IsCompleted", data.IsCompleted);
            dbparams.Add("@UserId", data.UserId);
            dbparams.Add("@IsApproved", data.IsApproved);
            dbparams.Add("@DistrictCode", data.DistrictCode);
            dbparams.Add("@DIPBUComment", data.DIPBUComment);
            dbparams.Add("@DIPCircuitComent", data.DIPCircuitComent);
            dbparams.Add("@DIPDirectComment", data.DIPDirectComment);
            dbparams.Add("@PIPBuComment", data.PIPBuComment);
            dbparams.Add("@PIPLineDirectorComment", data.PIPLineDirectorComment);
            dbparams.Add("@PIPChiefComment", data.PIPChiefComment);
            dbparams.Add("@PIPDepComment", data.PIPDepComment);

            var updateSIPActionPlan = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSIPActionPlanKPI]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateSIPActionPlan;

        }

        [HttpPost(nameof(UpdateSIPActionPlan))]
        public Task<int> UpdateSIPActionPlan(SIPActionPlanModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CompletedJSON", data.CompletedJSON);
            dbparams.Add("@EmisNumber", data.EmisNumber);
            dbparams.Add("@UserId", data.UserId);
            dbparams.Add("@Status", data.Status);

            var updateSIPActionPlan = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSIPActionPlan]", dbparams,commandType: CommandType.StoredProcedure));
            return updateSIPActionPlan;

        }

        [HttpPost(nameof(UpdateSIPActionPlanProgress))]
        public async Task<int> UpdateSIPActionPlanProgress(SIPActionPlanModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SIPId", data.SIPActionId);
            dbparams.Add("@ProgressPerQuarter", data.ProgressPerQuarter);
            dbparams.Add("@Quarter", data.Quarter);
            dbparams.Add("@IsApproved", data.IsApproved);
            dbparams.Add("@IsCompleted", data.IsCompleted);

            var updateSIPActionPlan = await Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSIPActionPlanProgress]", dbparams, commandType: CommandType.StoredProcedure));
            //return updateSIPActionPlan;

            var retVal = dbparams.Get<int>("@SIPId");
            return retVal;

        }


        [HttpPost(nameof(UpdateSchoolScore))]
        public Task<int> UpdateSchoolScore(SchoolKPIModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SchoolKPIID", data.SchoolKPIID, DbType.Int32);
            dbparams.Add("@Score", data.Score);
            dbparams.Add("@UserId", data.UserID);

            var updateSIPActionPlan = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSchoolReprofiling]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateSIPActionPlan;

        }

        [HttpPost(nameof(UpdateSipActionPlanAll))]
        public Task<int> UpdateSipActionPlanAll(SIPActionPlanModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CompletedJSON", data.CompletedJSON);

            var updateSIPActionPlan = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSipActionPlanAll]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateSIPActionPlan;

        }
    }
}
