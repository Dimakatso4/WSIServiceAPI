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
    public class HeadOfficeSubActivityPlanController : ControllerBase
    {
        private readonly IDapper _dapper;
        public HeadOfficeSubActivityPlanController(IDapper dapper)
        {
            _dapper = dapper;
        }
        //Creation of the subactivity
        [HttpPost(nameof(CreateHeadOfficeSubActivityPlanActivity))]
        public async Task<int> CreateHeadOfficeSubActivityPlanActivity(HeadOfficeSubActivityPlanModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@Id", data.Id, DbType.Int32);
            dataBaseParams.Add("@SubActivity", data.SubActivity);
            dataBaseParams.Add("@Responsibility", data.Responsibility);
            dataBaseParams.Add("@StartDate", data.StartDate, DbType.Date);
            dataBaseParams.Add("@EndDate", data.EndDate, DbType.Date);
            dataBaseParams.Add("@ManagementPlanActivityId", data.ManagementPlanActivityId, DbType.Int32);
            dataBaseParams.Add("@StatusID", data.StatusID);
            dataBaseParams.Add("@PeriodID", data.PeriodID);
            dataBaseParams.Add("@Branch", data.Branch);
            dataBaseParams.Add("@Directorate", data.Directorate);
            dataBaseParams.Add("@SubDirectorate", data.SubDirectorate);
            dataBaseParams.Add("@Region", data.Region);
            dataBaseParams.Add("@District", data.District);
            dataBaseParams.Add("@ChiefDirectorate", data.ChiefDirectorate);
            dataBaseParams.Add("@OfficeLevel", data.OfficeLevel);
            dataBaseParams.Add("@ResponsibilityType", data.ResponsibilityType);



            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddHeadOfficeSubActivityPlan]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var NewActivity = dataBaseParams.Get<int>("Id");
            return NewActivity;
        }
        //Get District Management Plan List 
        [HttpGet(nameof(GetHeadOfficeSubActivityPlanList))]
        public Task<List<HeadOfficeSubActivityPlanModel>> GetHeadOfficeSubActivityPlanList()
        {
            var DistrictManagementPlanList = Task.FromResult(_dapper.GetAll<HeadOfficeSubActivityPlanModel>($"select dmp.*,p.ManagementPeriod, mps.Status from [dbo].tblHeadOfficeSubActivityPlan  dmp INNER JOIN [dbo].[tblManagementPeriod] p ON p.ManagementPeriodID = dmp.PeriodID INNER JOIN [dbo].[tblManagementPlanStatus] mps ON mps.StatusID = dmp.StatusID where mps.StatusID = 4", null,
                    commandType: CommandType.Text));
            return DistrictManagementPlanList;
        }

        [HttpGet(nameof(GetHeadOfficeSubActivityPlanListByDistrictCode))]
        public Task<List<HeadOfficeSubActivityPlanModel>> GetHeadOfficeSubActivityPlanListByDistrictCode(string DistrictCode)
        {
            var DistrictManagementPlanList = Task.FromResult(_dapper.GetAll<HeadOfficeSubActivityPlanModel>($"select dmp.*,p.ManagementPeriod, mps.Status from [dbo].tblHeadOfficeSubActivityPlan  dmp INNER JOIN [dbo].[tblManagementPeriod] p ON p.ManagementPeriodID = dmp.PeriodID INNER JOIN [dbo].[tblManagementPlanStatus] mps ON mps.StatusID = dmp.StatusID where DistrictCode = '{DistrictCode}'", null,
                    commandType: CommandType.Text));
            return DistrictManagementPlanList;
        }
        //Get District Management Plan By Main Activity : Requires two parameters to produce accurate data
        [HttpGet(nameof(GetHeadOfficeSubActivityPlanByMainActivity))]
        public Task<List<HeadOfficeSubActivityPlanModel>> GetHeadOfficeSubActivityPlanByMainActivity(int MainActivityID)
        {
            var DistrictManagementPlanList = Task.FromResult(_dapper.GetAll<HeadOfficeSubActivityPlanModel>($"select dmp.*,p.ManagementPeriod, mps.Status from [dbo].tblHeadOfficeSubActivityPlan  dmp INNER JOIN [dbo].[tblManagementPeriod] p ON p.ManagementPeriodID = dmp.PeriodID INNER JOIN [dbo].[tblManagementPlanStatus] mps ON mps.StatusID = dmp.StatusID  WHere dmp.ManagementPlanActivityId = {MainActivityID}", null,
                    commandType: CommandType.Text));
            return DistrictManagementPlanList;
        }
        //Get District Management Plan By ID for update purposes
        [HttpGet(nameof(GetHeadOfficeSubActivityPlanByID))]
        public Task<List<HeadOfficeSubActivityPlanModel>> GetHeadOfficeSubActivityPlanByID(int ID)
        {
            var DistrictManagementPlan = Task.FromResult(_dapper.GetAll<HeadOfficeSubActivityPlanModel>($"select dmp.*,p.ManagementPeriod, mps.Status from [dbo].tblHeadOfficeSubActivityPlan  dmp INNER JOIN [dbo].[tblManagementPeriod] p ON p.ManagementPeriodID = dmp.PeriodID INNER JOIN [dbo].[tblManagementPlanStatus] mps ON mps.StatusID = dmp.StatusID  WHere dmp.Id = {ID} ", null,
                    commandType: CommandType.Text));

            foreach (HeadOfficeSubActivityPlanModel managementPlanModel in DistrictManagementPlan.Result)
            {

                var managementPlanUserRoleList = Task.FromResult(_dapper.GetAll<UserRoleModel>($"SELECT Id,RoleId,rolename FROM [dbo].[tblUserRole] WHERE Id in(SELECT n FROM intlist_to_tbl((select Responsibility from [dbo].tblHeadOfficeSubActivityPlan where ID={ID}),','))", null,
                   commandType: CommandType.Text));

                managementPlanModel.ResponsibilityList = managementPlanUserRoleList.Result;

            }

            return DistrictManagementPlan;
        }
        //Update using Patch Method
        //[HttpPost(nameof(UpdateDistrictManagementPlan))]
        //public Task<int> UpdateDistrictManagementPlan(DistrictManagementPlanModel data)
        //{
        //    var dataBaseParams = new DynamicParameters();
        //    dataBaseParams.Add("@Id", data.Id, DbType.Int32);
        //    dataBaseParams.Add("@SubActivity", data.SubActivity);
        //    dataBaseParams.Add("@Responsibility", data.Responsibility);
        //    dataBaseParams.Add("@StartDate", data.StartDate, DbType.Date);
        //    dataBaseParams.Add("@EndDate", data.EndDate, DbType.Date);
        //    dataBaseParams.Add("@ManagementPlanActivityId", data.ManagementPlanActivityId, DbType.Int32);
        //    dataBaseParams.Add("@DistrictCode", data.DistrictCode);
        //    dataBaseParams.Add("@Period", data.PeriodID);
        //    dataBaseParams.Add("@StatusID", data.StatusID);
            
        //    var updateAP = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateDistrictManagementPlan]",
        //                    dataBaseParams,
        //                    commandType: CommandType.StoredProcedure));
        //    return updateAP;
        //}
        //Update using Post Method same results as the above
        [HttpPost(nameof(UpdateHeadOfficeSubActivityPlan))]
        public Task<int> UpdateHeadOfficeSubActivityPlan(int Id, String SubActivity, string Responsibility, DateTime StartDate, DateTime EndDate, int ManagementPlanActivityId, int PeriodID, int StatusID
            ,string Branch,string Directorate,string SubDirectorate, string Region,string District, string ChiefDirectorate, string OfficeLevel, string ResponsibilityType)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@Id", Id, DbType.Int32);
            dataBaseParams.Add("@SubActivity", SubActivity);
            dataBaseParams.Add("@Responsibility", Responsibility);
            dataBaseParams.Add("@StartDate", StartDate, DbType.Date);
            dataBaseParams.Add("@EndDate", EndDate, DbType.Date);
            dataBaseParams.Add("@ManagementPlanActivityId", ManagementPlanActivityId, DbType.Int32);
            dataBaseParams.Add("@Period", PeriodID);
            dataBaseParams.Add("@StatusID", StatusID);
            dataBaseParams.Add("@Branch", Branch);
            dataBaseParams.Add("@Directorate", Directorate);
            dataBaseParams.Add("@SubDirectorate", SubDirectorate);
            dataBaseParams.Add("@Region", Region);
            dataBaseParams.Add("@District", District);
            dataBaseParams.Add("@ChiefDirectorate", ChiefDirectorate);
            dataBaseParams.Add("@OfficeLevel", OfficeLevel);
            dataBaseParams.Add("@ResponsibilityType", ResponsibilityType);
            var updateAP = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateHeadOfficeSubActivityPlan]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateAP;
        }
        //Update using Post Method same results as the above
        [HttpPost(nameof(UpdateStatusById))]
        public Task<int> UpdateStatusById(int Id, int StatusID)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@Id", Id, DbType.Int32);
            dataBaseParams.Add("@StatusID", StatusID, DbType.Int32);
          

            var updateAP = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateStatusById]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateAP;
        }


    }
}
