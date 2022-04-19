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
    public class UserTypeController : ControllerBase
    {
        private readonly IDapper _dapper;
        public UserTypeController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateUserType))]
        public async Task<int> CreateUserType(UserTypeModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@UserTypeId", data.UserTypeId, DbType.Int32);
            dataBaseParams.Add("@UserType", data.UserType);
            dataBaseParams.Add("@SchoolPrinicipal", data.SchoolPrinicipal);
            dataBaseParams.Add("@DistrictUsers", data.UserType);
            dataBaseParams.Add("@HeadOfficeUsers", data.UserType);


            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddUserType]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("UserTypeId");
            return returnVal;
        }
        [HttpGet(nameof(GetUserTypeList))]
        public Task<List<UserTypeModel>> GetUserTypeList()
        {
            var ManagementPlanList = Task.FromResult(_dapper.GetAll<UserTypeModel>($"select * from [dbo].tblUserType", null,
                    commandType: CommandType.Text));
            return ManagementPlanList;
        }

        [HttpPatch(nameof(UpdateUserType))]
        public Task<int> UpdateUserType(UserTypeModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@UserTypeId", data.UserTypeId, DbType.Int32);
            dataBaseParams.Add("@UserType", data.UserType);
            dataBaseParams.Add("@SchoolPrinicipal", data.SchoolPrinicipal);
            dataBaseParams.Add("@DistrictUsers", data.UserType);
            dataBaseParams.Add("@HeadOfficeUsers", data.UserType);

            var updateAP = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateUserType]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateAP;
        }
    }
}
