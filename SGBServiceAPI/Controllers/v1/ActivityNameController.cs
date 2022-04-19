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

namespace WSIServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityNameController : ControllerBase
    {
        private readonly IDapper _dapper;
        public ActivityNameController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateActivityName))]
        public async Task<int> CreateActivityName(ActivityNameModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@ActivityID", data.ActivityID, DbType.Int32);
            dataBaseParams.Add("@ActivityName", data.ActivityName);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddActivityName]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var ActivityName = dataBaseParams.Get<int>("ActivityID");
            return ActivityName;
        }
        [HttpGet(nameof(GetActivityNameList))]
        public Task<List<ActivityNameModel>> GetActivityNameList()
        {
            var ActivityNameList = Task.FromResult(_dapper.GetAll<ActivityNameModel>($"select * from [dbo].[tblActivityName]", null,
                    commandType: CommandType.Text));
            return ActivityNameList;
        }
        [HttpGet(nameof(GetActivityNameByID))]
        public Task<List<ActivityNameModel>> GetActivityNameByID(int ID)
        {
            var ActivityName = Task.FromResult(_dapper.GetAll<ActivityNameModel>($"select [ActivityName] from [dbo].[tblActivityName] where [ActivityID] = {ID}", null,
                    commandType: CommandType.Text));
            return ActivityName;
        }

    }
}
