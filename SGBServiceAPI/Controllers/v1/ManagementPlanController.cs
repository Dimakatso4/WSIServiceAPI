using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using WSIServiceAPI.Models;
using System.Data;
using WSIServiceAPI.Services;

namespace WSIServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementPlanController : ControllerBase
    {
        private readonly IDapper _dapper;
        public ManagementPlanController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateManagementPlan))]
        public async Task<bool> CreateManagementPlan(List<ManagementPlanModel> managementPlanList)
        {
            bool returnVal = false;
            foreach (ManagementPlanModel data in managementPlanList) {
                var dataBaseParams = new DynamicParameters();
                dataBaseParams.Add("@PlanID", data.PlanID, DbType.Int32, ParameterDirection.InputOutput);
                dataBaseParams.Add("@ActivityName", data.ActivityName);
                dataBaseParams.Add("@Responsibility", data.ResponsibilityId);
                dataBaseParams.Add("@StartDate", data.StartDate, DbType.Date);
                dataBaseParams.Add("@EndDate", data.EndDate, DbType.Date);
                dataBaseParams.Add("@StatusID", data.StatusID);
                dataBaseParams.Add("@Comment", data.Comment);
                dataBaseParams.Add("@PeriodID", data.PeriodID);
                dataBaseParams.Add("@Branch", data.Branch);
                dataBaseParams.Add("@Directorate", data.Directorate);
                dataBaseParams.Add("@SubDirectorate", data.SubDirectorate);
                dataBaseParams.Add("@Region", data.Region);
                dataBaseParams.Add("@District", data.District);
                dataBaseParams.Add("@ChiefDirectorate", data.ChiefDirectorate);
                dataBaseParams.Add("@OfficeLevel", data.OfficeLevel);
                dataBaseParams.Add("@ResponsibilityType", data.ResponsibilityType);
                







                var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddManagementPlan]"
                    , dataBaseParams,
                    commandType: CommandType.StoredProcedure));
                var results = dataBaseParams.Get<int>("PlanID");
                returnVal = results != 0;

                dataBaseParams = null;
            }
            return returnVal;
        }
        [HttpGet(nameof(GetActivityPlanById))]
        public Task<List<DistrictManagementPlanModel>> GetActivityPlanById(int PlanID)
        {
            var DistrictManagementPlanList = Task.FromResult(_dapper.GetAll<DistrictManagementPlanModel>($"select dmp.*,p.ManagementPeriod, mps.Status from [dbo].tblManagementPlan  dmp INNER JOIN [dbo].[tblManagementPeriod] p ON p.ManagementPeriodID = dmp.PeriodID INNER JOIN [dbo].[tblManagementPlanStatus] mps ON mps.StatusID = dmp.StatusID where PlanID = '{PlanID}'", null,
                    commandType: CommandType.Text));
            return DistrictManagementPlanList;
        }
        [HttpGet(nameof(GetManagementPlanList))]
        public Task<List<ManagementPlanModel>> GetManagementPlanList()
        {
            var managementPlanList = Task.FromResult(_dapper.GetAll<ManagementPlanModel>($"SELECT PlanID,ManageP.PeriodID,ActivityName,StartDate,EndDate,[dbo].[tblManagementPlanStatus].StatusID,Comment,[dbo].[tblManagementPlanStatus].Status, mp.ManagementPeriod  FROM [dbo].tblManagementPlan ManageP INNER JOIN [dbo].[tblManagementPlanStatus] On [dbo].[tblManagementPlanStatus].StatusID = ManageP.StatusId INNER JOIN [dbo].[tblManagementPeriod] mp ON mp.ManagementPeriodID = ManageP.PeriodID WHERE ManageP.[StatusID] <> 10  Order by StartDate", null,
                    commandType: CommandType.Text));

            foreach(ManagementPlanModel managementPlanModel in managementPlanList.Result)
            {

                var managementPlanUserRoleList = Task.FromResult(_dapper.GetAll<UserRoleModel>($"SELECT Id, RoleId,rolename FROM [dbo].[tblUserRole] WHERE Id in(SELECT n FROM intlist_to_tbl((select Responsibility from [dbo].tblManagementPlan where PlanID={managementPlanModel.PlanID}),','))", null,
                   commandType: CommandType.Text));

                managementPlanModel.Responsibility = managementPlanUserRoleList.Result;

            }

            return managementPlanList;
        }

        [HttpGet(nameof(GetManagementPlanListForCES))]
        public Task<List<ManagementPlanModel>> GetManagementPlanListForCES()
        {
            var managementPlanList = Task.FromResult(_dapper.GetAll<ManagementPlanModel>($"SELECT PlanID,ManageP.PeriodID,ActivityName,StartDate,EndDate,[dbo].[tblManagementPlanStatus].StatusID,Comment,[dbo].[tblManagementPlanStatus].Status, mp.ManagementPeriod  FROM [dbo].tblManagementPlan ManageP INNER JOIN [dbo].[tblManagementPlanStatus] On [dbo].[tblManagementPlanStatus].StatusID = ManageP.StatusId INNER JOIN [dbo].[tblManagementPeriod] mp ON mp.ManagementPeriodID = ManageP.PeriodID WHERE ManageP.[StatusID] = 10 or ManageP.[StatusID] = 5 or ManageP.[StatusID] = 6  or ManageP.[StatusID] = 11  or ManageP.[StatusID] = 12  or ManageP.[StatusID] = 13  or ManageP.[StatusID] = 14  or ManageP.[StatusID] = 4 or ManageP.[StatusID] = 15  Order by StartDate", null,
                    commandType: CommandType.Text));

            foreach (ManagementPlanModel managementPlanModel in managementPlanList.Result)
            {

                var managementPlanUserRoleList = Task.FromResult(_dapper.GetAll<UserRoleModel>($"SELECT Id,RoleId,rolename FROM [dbo].[tblUserRole] WHERE Id in(SELECT n FROM intlist_to_tbl((select Responsibility from [dbo].tblManagementPlan where PlanID={managementPlanModel.PlanID}),','))", null,
                   commandType: CommandType.Text));

                managementPlanModel.Responsibility = managementPlanUserRoleList.Result;

            }

            return managementPlanList;
        }

        [HttpGet(nameof(GetManagementPlanListForDirector))]
        public Task<List<ManagementPlanModel>> GetManagementPlanListForDirector()
        {
            var managementPlanList = Task.FromResult(_dapper.GetAll<ManagementPlanModel>($"SELECT PlanID,ManageP.PeriodID,ActivityName,StartDate,EndDate,[dbo].[tblManagementPlanStatus].StatusID,Comment,[dbo].[tblManagementPlanStatus].Status, mp.ManagementPeriod  FROM [dbo].tblManagementPlan ManageP INNER JOIN [dbo].[tblManagementPlanStatus] On [dbo].[tblManagementPlanStatus].StatusID = ManageP.StatusId INNER JOIN [dbo].[tblManagementPeriod] mp ON mp.ManagementPeriodID = ManageP.PeriodID WHERE ManageP.[StatusID] <> 10 and ManageP.[StatusID] = 3 or ManageP.[StatusID] = 4  or ManageP.[StatusID] = 8  or ManageP.[StatusID] = 9  or ManageP.[StatusID] = 1 or ManageP.[StatusID] = 2  Order by StartDate", null,
                    commandType: CommandType.Text));

            foreach (ManagementPlanModel managementPlanModel in managementPlanList.Result)
            {

                var managementPlanUserRoleList = Task.FromResult(_dapper.GetAll<UserRoleModel>($"SELECT Id,RoleId,rolename FROM [dbo].[tblUserRole] WHERE Id in(SELECT n FROM intlist_to_tbl((select Responsibility from [dbo].tblManagementPlan where PlanID={managementPlanModel.PlanID}),','))", null,
                   commandType: CommandType.Text));

                managementPlanModel.Responsibility = managementPlanUserRoleList.Result;

            }

            return managementPlanList;
        }

        [HttpGet(nameof(GetManagementPlanListRequestUpdate))]
        public Task<List<ManagementPlanModel>> GetManagementPlanListRequestUpdate()
        {
            var managementPlanList = Task.FromResult(_dapper.GetAll<ManagementPlanModel>($"SELECT PlanID,ManageP.PeriodID,ActivityName,StartDate,EndDate,[dbo].[tblManagementPlanStatus].StatusID,Comment,[dbo].[tblManagementPlanStatus].Status, mp.ManagementPeriod  FROM [dbo].tblManagementPlan ManageP INNER JOIN [dbo].[tblManagementPlanStatus] On [dbo].[tblManagementPlanStatus].StatusID = ManageP.StatusId INNER JOIN [dbo].[tblManagementPeriod] mp ON mp.ManagementPeriodID = ManageP.PeriodID WHERE ManageP.[StatusID] <> 10 and ManageP.[StatusID] = 1  Order by StartDate", null,
                    commandType: CommandType.Text));

            foreach (ManagementPlanModel managementPlanModel in managementPlanList.Result)
            {

                var managementPlanUserRoleList = Task.FromResult(_dapper.GetAll<UserRoleModel>($"SELECT Id,RoleId,rolename FROM [dbo].[tblUserRole] WHERE Id in(SELECT n FROM intlist_to_tbl((select Responsibility from [dbo].tblManagementPlan where PlanID={managementPlanModel.PlanID}),','))", null,
                   commandType: CommandType.Text));

                managementPlanModel.Responsibility = managementPlanUserRoleList.Result;

            }

            return managementPlanList;
        }

        [HttpGet(nameof(GetManagementPlanListByAprpoved))]
        public Task<List<ManagementPlanModel>> GetManagementPlanListByAprpoved()
        {
            var managementPlanList = Task.FromResult(_dapper.GetAll<ManagementPlanModel>($"SELECT PlanID,ManageP.PeriodID,ActivityName,StartDate,EndDate,[dbo].[tblManagementPlanStatus].StatusID,Comment,[dbo].[tblManagementPlanStatus].Status, mp.ManagementPeriod  FROM [dbo].tblManagementPlan ManageP INNER JOIN [dbo].[tblManagementPlanStatus] On [dbo].[tblManagementPlanStatus].StatusID = ManageP.StatusId INNER JOIN [dbo].[tblManagementPeriod] mp ON mp.ManagementPeriodID = ManageP.PeriodID WHERE ManageP.[StatusID] <> 10 and ManageP.[StatusID] = 2 or ManageP.[StatusID] = 1  Order by StartDate", null,
                    commandType: CommandType.Text));

            foreach (ManagementPlanModel managementPlanModel in managementPlanList.Result)
            {

                var managementPlanUserRoleList = Task.FromResult(_dapper.GetAll<UserRoleModel>($"SELECT Id,RoleId,rolename FROM [dbo].[tblUserRole] WHERE Id in(SELECT n FROM intlist_to_tbl((select Responsibility from [dbo].tblManagementPlan where PlanID={managementPlanModel.PlanID}),','))", null,
                   commandType: CommandType.Text));

                managementPlanModel.Responsibility = managementPlanUserRoleList.Result;

            }

            return managementPlanList;
        }

        [HttpGet(nameof(GetManagementPlanListPublished))]
        public Task<List<ManagementPlanModel>> GetManagementPlanListPublished()
        {
            var managementPlanList = Task.FromResult(_dapper.GetAll<ManagementPlanModel>($"SELECT PlanID,ManageP.PeriodID,ActivityName,StartDate,EndDate,[dbo].[tblManagementPlanStatus].StatusID,Comment,[dbo].[tblManagementPlanStatus].Status, mp.ManagementPeriod  FROM [dbo].tblManagementPlan ManageP INNER JOIN [dbo].[tblManagementPlanStatus] On [dbo].[tblManagementPlanStatus].StatusID = ManageP.StatusId INNER JOIN [dbo].[tblManagementPeriod] mp ON mp.ManagementPeriodID = ManageP.PeriodID WHERE ManageP.[StatusID] <> 10 and ManageP.[StatusID] = 4  Order by StartDate", null,
                    commandType: CommandType.Text));

            foreach (ManagementPlanModel managementPlanModel in managementPlanList.Result)
            {

                var managementPlanUserRoleList = Task.FromResult(_dapper.GetAll<UserRoleModel>($"SELECT Id,RoleId,rolename FROM [dbo].[tblUserRole] WHERE Id in(SELECT n FROM intlist_to_tbl((select Responsibility from [dbo].tblManagementPlan where PlanID={managementPlanModel.PlanID}),','))", null,
                   commandType: CommandType.Text));

                managementPlanModel.Responsibility = managementPlanUserRoleList.Result;

            }

            return managementPlanList;
        }

        [HttpGet(nameof(ViewManagementPlanByID))]
        public Task<List<ManagementPlanModel>> ViewManagementPlanByID(int ID)
        {
            var managementPlanList = Task.FromResult(_dapper.GetAll<ManagementPlanModel>($"SELECT PlanID,ActivityName,StartDate,EndDate,Branch,Directorate,SubDirectorate,Region,District,ChiefDirectorate ,OfficeLevel,ResponsibilityType,[dbo].[tblManagementPlanStatus].StatusID,Comment,[dbo].[tblManagementPlanStatus].Status, mp.ManagementPeriod  FROM [dbo].tblManagementPlan ManageP INNER JOIN [dbo].[tblManagementPlanStatus] On [dbo].[tblManagementPlanStatus].StatusID = ManageP.StatusId INNER JOIN [dbo].[tblManagementPeriod] mp ON mp.ManagementPeriodID = ManageP.PeriodID WHERE  ManageP.PlanID = {ID}", null,
                    commandType: CommandType.Text));

            foreach (ManagementPlanModel managementPlanModel in managementPlanList.Result)
            {

                var managementPlanUserRoleList = Task.FromResult(_dapper.GetAll<UserRoleModel>($"SELECT Id,RoleId,rolename FROM [dbo].[tblUserRole] WHERE Id in(SELECT n FROM intlist_to_tbl((select Responsibility from [dbo].tblManagementPlan where PlanID={managementPlanModel.PlanID}),','))", null,
                   commandType: CommandType.Text));

                managementPlanModel.Responsibility = managementPlanUserRoleList.Result;

            }

            return managementPlanList;
        }

        [HttpGet(nameof(ManagementPlanGetByActivityName))]
        public Task<ManagementPlanModel> ManagementPlanGetByActivityName(string ActivityName, int periodID)
        {
            var ManagementPlanList = Task.FromResult(_dapper.Get<ManagementPlanModel>($"select PlanID, activityName,periodID  from [dbo].[tblManagementPlan] where [ActivityName] = '{ActivityName}' and PeriodID = {periodID}", null,
                    commandType: CommandType.Text));
            return ManagementPlanList;
        }

        [HttpPost(nameof(UpdateReviewActivityPlanByID))]
        public Task<int> UpdateReviewActivityPlanByID(ManagementPlanModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@PlanID", data.PlanID, DbType.Int32);
            dataBaseParams.Add("@ActivityName", data.ActivityName);
            dataBaseParams.Add("@Responsibility", data.Responsibility);
            dataBaseParams.Add("@StartDate", data.StartDate, DbType.Date);
            dataBaseParams.Add("@EndDate", data.EndDate, DbType.Date);
            dataBaseParams.Add("@Comment", data.Comment);
            dataBaseParams.Add("@StatusID", data.StatusID);
            dataBaseParams.Add("@PeriodID", data.PeriodID);


            var updateAP = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateManagementPlan]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateAP;
        }

        [HttpPost(nameof(UpdateStatusByPlanID))]
        public Task<int> UpdateStatusByPlanID(int PlanID, int StatusID)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@PlanID", PlanID, DbType.Int32);
            dataBaseParams.Add("@StatusID", StatusID);

            var updateAP = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateManagementPlanByPlanID]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateAP;
        }

        [HttpPost(nameof(CreateManagementDocument))]
        public async Task<int> CreateManagementDocument(ManagementDocumentModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@DocumentID", data.DocumentID, DbType.Int32);
            dataBaseParams.Add("@DocumentName", data.DocumentName);
            dataBaseParams.Add("@DocumentPath", data.DocumentPath);
            dataBaseParams.Add("@DocumentDateUploaded", data.DocumentDateUploaded, DbType.Date);
            dataBaseParams.Add("@UploadedBy", data.UploadedBy);
            dataBaseParams.Add("@YearUploaded", data.YearUploaded);


            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddManagentDocument]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("DocumentID");
            return returnVal;
        }
        [HttpGet(nameof(GetAllManagementDocuments))]
        public Task<List<ManagementDocumentModel>> GetAllManagementDocuments()
        {
            var ManagementDocsList = Task.FromResult(_dapper.GetAll<ManagementDocumentModel>($"SELECT * FROM [dbo].[tblManagementDocument]", null,
                    commandType: CommandType.Text));
            return ManagementDocsList;

        }
        [HttpGet(nameof(ViewManagementDocumentByID))]
        public Task<ManagementDocumentModel> ViewManagementDocumentByID(int ID)
        {
            var ManagementDoc = Task.FromResult(_dapper.Get<ManagementDocumentModel>($"SELECT DISTINCT * FROM [dbo].[tblManagementDocument] WHERE [DocumentID] = {ID}", null,
                    commandType: CommandType.Text));
            return ManagementDoc;
        }

        [HttpPatch(nameof(UpdateManagementDocumentByID))]
        public Task<int> UpdateManagementDocumentByID(ManagementDocumentModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@DocumentID", data.DocumentID, DbType.Int32);
            dataBaseParams.Add("@DocumentName", data.DocumentName);
            dataBaseParams.Add("@DocumentPath", data.DocumentPath);
            dataBaseParams.Add("@DocumentDateUploaded", data.DocumentDateUploaded, DbType.Date);
            dataBaseParams.Add("@UploadedBy", data.UploadedBy);
            dataBaseParams.Add("@YearUploaded", data.YearUploaded);


            var updateAP = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateManagementDocument]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateAP;
        }
        [HttpPost(nameof(UpdateActivityPlan))]
        public Task<int> UpdateActivityPlan(int PlanID, string ActivityName, string Responsibility, DateTime StartDate, DateTime EndDate, int StatusID, string Comment)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@PlanID", PlanID);
            dataBaseParams.Add("@ActivityName", ActivityName);
            dataBaseParams.Add("@Responsibility", Responsibility);
            dataBaseParams.Add("@StartDate", StartDate);
            dataBaseParams.Add("@EndDate", EndDate);
            dataBaseParams.Add("@StatusID", StatusID);
            dataBaseParams.Add("@Comment", Comment);

            var updateAP = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateManagementPlan]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateAP;
        }

        [HttpGet(nameof(GetHeadOfficeBranchByID))]
        public Task<List<HeadOfficeBranchModel>> GetHeadOfficeBranchByID(string IDs)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@OfficeBranchIds", IDs);

            var ManagementDoc = Task.FromResult(_dapper.GetAll<HeadOfficeBranchModel>("[dbo].[SP_GetHeadOfficeBranch]", dataBaseParams,
                    commandType: CommandType.StoredProcedure));
            return ManagementDoc;
        }

        [HttpGet(nameof(GetChiefDirectorateByBranchID))]
        public Task<List<ChiefDirectorateModel>> GetChiefDirectorateByBranchID(string IDs)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@BranchIds", IDs);

            var ManagementDoc = Task.FromResult(_dapper.GetAll<ChiefDirectorateModel>("[dbo].[SP_GetChiefDirectorateByBranchID]", dataBaseParams,
                    commandType: CommandType.StoredProcedure));
            return ManagementDoc;
        }

        [HttpGet(nameof(GetDirectorateByChiefDirectory))]
        public Task<List<DirectorateModel>> GetDirectorateByChiefDirectory(string IDs)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@ChiefDirectoryIds", IDs);

            var ManagementDoc = Task.FromResult(_dapper.GetAll<DirectorateModel>("[dbo].[SP_GetDirectorateByChiefDirectoryID]", dataBaseParams,
                    commandType: CommandType.StoredProcedure));
            return ManagementDoc;
        }

        [HttpGet(nameof(GetSubDirectorateByDirectorateID))]
        public Task<List<SubDirectorateModel>> GetSubDirectorateByDirectorateID(string IDs)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@ChiefDirectoryIds", IDs);

            var ManagementDoc = Task.FromResult(_dapper.GetAll<SubDirectorateModel>("[dbo].[SP_GetSubDirectorateByDirectorateID]", dataBaseParams,
                    commandType: CommandType.StoredProcedure));
            return ManagementDoc;
        }

    }
}
