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
    public class SchoolController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SchoolController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateSchool))]
        public async Task<int> CreateSchool(SchoolModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@SchoolId", data.SchoolId, DbType.Int32);
            dataBaseParams.Add("@SchoolName", data.SchoolName);
            dataBaseParams.Add("@SchoolCode", data.SchoolCode);
            dataBaseParams.Add("@SchoolAddress", data.SchoolAddress);
            dataBaseParams.Add("@District", data.District);
            dataBaseParams.Add("@ClusterNumber", data.ClusterNumber);
            dataBaseParams.Add("@TellNumber", data.TellNumber, DbType.Int32);
            dataBaseParams.Add("@EmailAddress", data.EmailAddress);
            dataBaseParams.Add("@FaxNumber", data.FaxNumber, DbType.Int32);
            dataBaseParams.Add("@CellphoneNumber", data.CellphoneNumber, DbType.Int32);
            dataBaseParams.Add("@SchoolType", data.SchoolType);
            dataBaseParams.Add("@Province", data.Province);
            dataBaseParams.Add("@Code", data.Code, DbType.Int32);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddSchool]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("SchoolId");
            return returnVal;
        }
        [HttpGet(nameof(GetSchool))]
        public Task<List<SchoolModel>> GetSchool()
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolModel>($"select[SchoolId],[SchoolName],[SchoolCode],[SchoolAddress],[District],[ClusterNumber],[SchoolType],[dbo].[tblDistrictWSI].[Region] from tblSchool join tblDistrictWSI on tblSchool .District = tblDistrictWSI.DistrictName", null,
            commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetSchoolName))]
        public Task<List<SchoolModel>> GetSchoolName()
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolModel>($"select [SchoolName] from [dbo].[tblSchool]", null,
                    commandType: CommandType.Text));
            return Output;
        }


        [HttpGet(nameof(GetSchoolsNew))]
        public Task<List<SchoolModel>> GetSchoolsNew()
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolModel>($"select * from GDEMainData", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpPatch(nameof(updateSchool))]
        public Task<int> updateSchool(SchoolModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@SchoolId", data.SchoolId, DbType.Int32);
            dataBaseParams.Add("@SchoolName", data.SchoolName);
            dataBaseParams.Add("@SchoolCode", data.SchoolCode);
            dataBaseParams.Add("@SchoolAddress", data.SchoolAddress);
            dataBaseParams.Add("@District", data.District);
            dataBaseParams.Add("@ClusterNumber", data.ClusterNumber);
            dataBaseParams.Add("@TellNumber", data.TellNumber, DbType.Int32);
            dataBaseParams.Add("@EmailAddress", data.EmailAddress);
            dataBaseParams.Add("@FaxNumber", data.FaxNumber, DbType.Int32);
            dataBaseParams.Add("@CellphoneNumber", data.CellphoneNumber, DbType.Int32);
            dataBaseParams.Add("@SchoolType", data.SchoolType);
            dataBaseParams.Add("@Province", data.Province);
            dataBaseParams.Add("@Code", data.Code, DbType.Int32);

            var updateSchool = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSchool]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateSchool;

        }

        [HttpGet(nameof(GetSchoolByDistricts))]
        public Task<List<SchoolModel>> GetSchoolByDistricts(string districtcode)
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolModel>($"select [SchoolId],[SchoolName],[SchoolCode],[SchoolAddress],[District],[ClusterNumber],[SchoolType], dw.[Region], dw.DistrictCode from tblSchool school INNER join tblDistrictWSI dw on dw.DistrictName = school.District WHERE dw.DistrictCode = '{districtcode}'", null,
            commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetAllSchoolByDistricts))]
        public Task<List<SchoolModel>> GetAllSchoolByDistricts()
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolModel>($"select [SchoolId],[SchoolName],[SchoolCode],[SchoolAddress],[District],[ClusterNumber],[SchoolType], dw.[Region], dw.DistrictCode from tblSchool school INNER join tblDistrictWSI dw on dw.DistrictName = school.District", null,
            commandType: CommandType.Text));
            return Output;
        }


        [HttpGet(nameof(GetSchoolsByDistrict))]
        public Task<List<SchoolNamesModel>> GetSchoolsByDistrict(string DistrictCode)
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolNamesModel>($"SELECT * FROM [dbo].[tblSchools] WHERE [DistrictCode] = '{DistrictCode}'", null,
            commandType: CommandType.Text));
            return Output;
        }



        [HttpGet(nameof(GetKPIDIP))]
        public Task<List<SchoolKPIModel>> GetKPIDIP(string DistrictCode)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT  AreaOfEvaluationID ManageAreaOfEvaluationID,  ComponentID ManageComponentID, AreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG(AreaOfEvaluationID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, ComponentID) AS PreviousAreaOfEvaluation,[ComponentID] AS CurrentComponent, LAG(ComponentID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, [ComponentID]) AS PreviousComponent, Response, IsSip, IsPrioritise, Status,[tblManageAreaOfEvaluation].FocusArea ,[tblManageComponents].ComponentName,[KPIName], Comment, Evidence EvidenceDescription, Documentpath,EmisNumber EmisNo,DateCaptured,UserId,KPIID schoolKPIID FROM[dbo].[tblSchoolKPI] INNER JOIN dbo.[tblManageKPI]  ON[dbo].[tblManageKPI].ManageKPIID = [dbo].[tblSchoolKPI].KPIID Inner Join[dbo].[tblManageComponents] on[tblManageComponents].ManageComponentID = [tblSchoolKPI].ComponentID Inner Join[dbo].[tblManageAreaOfEvaluation] on[tblManageAreaOfEvaluation].ManageAreaOfEvalutionID = [tblSchoolKPI].AreaOfEvaluationID WHERE IsSip=1 AND Status='SIPPublished' Order By AreaOfEvaluationID", null, commandType: CommandType.Text));

            //for (int i = 0; i < SchoolResponse.Result.Count; i++)
            //{

            //    var Output = Task.FromResult(_dapper.GetAll<SchoolNamesModel>($"SELECT KPIID,tblSchoolKPI.EmisNumber EmisNumber,[InstitutionName] InstitutionName,[DistrictCode],[DistrictName] FROM tblSchoolKPI INNER JOIN [dbo].[tblSchools] ON [dbo].[tblSchools].EmisNumber = tblSchoolKPI.EmisNumber WHERE KPIID={SchoolResponse.Result[i].SchoolKPIID} AND DistrictCode='{DistrictCode}' ORDER BY KPIID,[dbo].[tblSchools].EmisNumber ", null,
            //    commandType: CommandType.Text));

            //    SchoolResponse.Result[i].DIPSchools = Output.Result;
            //}
            return SchoolResponse;
        }


        [HttpGet(nameof(GetSchoolsKPIDIP))]
        public Task<List<SchoolNamesModel>> GetSchoolsKPIDIP(string DistrictCode,int SchoolKPIID)
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolNamesModel>($"SELECT SchoolKPIID,tblSchoolKPI.EmisNumber EmisNumber,[InstitutionName] InstitutionName,[DistrictCode],[DistrictName] FROM tblSchoolKPI INNER JOIN [dbo].[tblSchools] ON [dbo].[tblSchools].EmisNumber = tblSchoolKPI.EmisNumber WHERE KPIID={SchoolKPIID} AND DistrictCode='{DistrictCode}' ORDER BY KPIID,[dbo].[tblSchools].EmisNumber", null, commandType: CommandType.Text));

            return SchoolResponse;
        }


        [HttpGet(nameof(GetKPIPIP))]
        public Task<List<SchoolKPIModel>> GetKPIPIP()
        {
            var SchoolResponse = Task.FromResult(_dapper.GetAll<SchoolKPIModel>($"SELECT  AreaOfEvaluationID ManageAreaOfEvaluationID,  ComponentID ManageComponentID, AreaOfEvaluationID AS CurrentAreaOfEvaluation, LAG(AreaOfEvaluationID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, ComponentID) AS PreviousAreaOfEvaluation,[ComponentID] AS CurrentComponent, LAG(ComponentID, 1, 0) OVER(ORDER BY AreaOfEvaluationID, [ComponentID]) AS PreviousComponent, Response, IsSip, IsPrioritise, Status,[tblManageAreaOfEvaluation].FocusArea ,[tblManageComponents].ComponentName,[KPIName], Comment, Evidence EvidenceDescription, Documentpath,EmisNumber EmisNo,DateCaptured,UserId,KPIID schoolKPIID FROM[dbo].[tblSchoolKPI] INNER JOIN dbo.[tblManageKPI]  ON[dbo].[tblManageKPI].ManageKPIID = [dbo].[tblSchoolKPI].KPIID Inner Join[dbo].[tblManageComponents] on[tblManageComponents].ManageComponentID = [tblSchoolKPI].ComponentID Inner Join[dbo].[tblManageAreaOfEvaluation] on[tblManageAreaOfEvaluation].ManageAreaOfEvalutionID = [tblSchoolKPI].AreaOfEvaluationID WHERE IsSip=1 AND Status='SIPPublished' Order By AreaOfEvaluationID", null, commandType: CommandType.Text));


            //for (int i = 0; i < SchoolResponse.Result.Count; i++)
            //{

                //var districts = Task.FromResult(_dapper.GetAll<DistrictModel>($"SELECT [DistrictCode],[DistrictName] FROM tblSchoolKPI INNER JOIN [dbo].[tblSchools] ON [dbo].[tblSchools].EmisNumber = tblSchoolKPI.EmisNumber WHERE KPIID={SchoolResponse.Result[i].SchoolKPIID} GROUP BY [DistrictCode],[DistrictName]", null,
                //commandType: CommandType.Text));

                //SchoolResponse.Result[i].Districts = districts.Result;

                //for (int indx = 0; indx < districts.Result.Count; indx++)
                //{

                //    var schools = Task.FromResult(_dapper.GetAll<SchoolNamesModel>($"SELECT KPIID,tblSchoolKPI.EmisNumber EmisNumber,[InstitutionName] InstitutionName,[DistrictCode],[DistrictName] FROM tblSchoolKPI INNER JOIN [dbo].[tblSchools] ON [dbo].[tblSchools].EmisNumber = tblSchoolKPI.EmisNumber WHERE KPIID={SchoolResponse.Result[indx].SchoolKPIID} ORDER BY KPIID,[dbo].[tblSchools].EmisNumber ", null,
                //    commandType: CommandType.Text));

                //    SchoolResponse.Result[i].Districts[indx].Schools = schools.Result;
                //}
            //}
            return SchoolResponse;
        }



        [HttpGet(nameof(GetSchoolByEmisCode))]
        public async Task<SchoolNamesModel> GetSchoolByEmisCode(string EmisCode)
        {
            var Output = await Task.FromResult(_dapper.Get<SchoolNamesModel>($"SELECT * FROM [dbo].[tblSchools] WHERE [EmisNumber] = '{EmisCode}'", null,
            commandType: CommandType.Text));
            return Output;
        }



    }
}
