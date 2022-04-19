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
    public class SchoolMainController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SchoolMainController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(GetMainData))]
        public Task<List<SchoolMainModel>> GetMainData()
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolMainModel>($"select  [Region],[DistrictName],[DistrictCode],[CircuitNo],[ClusterNo],[InstitutionName],[EmisNumber],[InstLevelBudgetaryRequirements],[Level],[TypeofInstitution],[Sector],[NoFee],[SchoolId] from[dbo].[GDEMainData]", null,
                    commandType: CommandType.Text));
            return Output;
        }


        [HttpGet(nameof(GetSchoolMainNames))]
        public Task<List<SchoolMainModel>> GetSchoolMainNames()
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolMainModel>($"select [Region], [DistrictName], [DistrictCode], [CircuitNo], [ClusterNo], [InstitutionName] from [dbo].[GDEMain]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetSchoolMainList))]
        public Task<List<SchoolMainModel>> GetSchoolMainList()
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolMainModel>($"select * from [dbo].[GDEMain]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetSchoolMainById))]
        public Task<List<SchoolMainModel>> GetSchoolMainById(int Id)
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolMainModel>($"select * from [dbo].[GDEMain] where Id={Id}", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpPost(nameof(CreateSchoolMain))]
        public async Task<int> CreateSchoolMain(SchoolMainModel data)
        {
            var dataBaseParams = new Dapper.DynamicParameters();
            dataBaseParams.Add("@Region", data.Region);
            dataBaseParams.Add("@DistrictName", data.DistrictName);
            dataBaseParams.Add("@DistrictCode", data.DistrictCode);
            dataBaseParams.Add("@CircuitNo", data.CircuitNo);
            dataBaseParams.Add("@ClusterNo", data.ClusterNo);
            dataBaseParams.Add("@InstitutionName", data.InstitutionName);
            dataBaseParams.Add("@EmisNumber", data.EmisNumber);
            dataBaseParams.Add("@InstLevelBudgetaryRequirements", data.InstLevelBudgetaryRequirements);
            dataBaseParams.Add("@Level", data.Level);
            dataBaseParams.Add("@TypeofInstitution", data.TypeofInstitution);
            dataBaseParams.Add("@Sector", data.Sector);
            dataBaseParams.Add("@NoFee", data.NoFee);
            dataBaseParams.Add("@Id", data.SchoolId, DbType.Int32);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddSchoolMain]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("Id");
            return returnVal;
        }
        [HttpPatch(nameof(updateSchoolMain))]
        public Task<int> updateSchoolMain(SchoolMainModel data)
        {
            var dataBaseParams = new Dapper.DynamicParameters();
            dataBaseParams.Add("@Region", data.Region);
            dataBaseParams.Add("@DistrictName", data.DistrictName);
            dataBaseParams.Add("@DistrictCode", data.DistrictCode);
            dataBaseParams.Add("@CircuitNo", data.CircuitNo);
            dataBaseParams.Add("@ClusterNo", data.ClusterNo);
            dataBaseParams.Add("@InstitutionName", data.InstitutionName);
            dataBaseParams.Add("@EmisNumber", data.EmisNumber);
            dataBaseParams.Add("@InstLevelBudgetaryRequirements", data.InstLevelBudgetaryRequirements);
            dataBaseParams.Add("@Level", data.Level);
            dataBaseParams.Add("@TypeofInstitution", data.TypeofInstitution);
            dataBaseParams.Add("@Sector", data.Sector);
            dataBaseParams.Add("@NoFee", data.NoFee);
            dataBaseParams.Add("@Id", data.SchoolId, DbType.Int32);

            var updateSchoolMain = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSchoolMain]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateSchoolMain;

        }

    }
}