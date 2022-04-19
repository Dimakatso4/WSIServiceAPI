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
    public class ManagementPlanCommentsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public ManagementPlanCommentsController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateManagementPlan))]
        public async Task<bool> CreateManagementPlan(ManagementPlanCommentsModel data)
        {
            var dataBaseParams = new Dapper.DynamicParameters();
            dataBaseParams.Add("@Comments", data.Comments);
            dataBaseParams.Add("@ManagementPlanID", data.ManagementPlanID);


            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddManagementCommentsPlan]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("@ManagementPlanCommentsID");

            return true;
        }
        [HttpGet(nameof(GetManagementPlanCommentsByPlanID))]
        public Task<List<ManagementPlanCommentsModel>> GetManagementPlanCommentsByPlanID(int PlanID)
        {
            //var result = Task.FromResult(_dapper.GetAll<NewsFeedModel>($" SELECT * from [dbo].[tblNewsFeed] where datediff(DAY, [dateGenerated], GETDATE()) <= 5 Order By DateGenerated DESC", null,
            var result = Task.FromResult(_dapper.GetAll<ManagementPlanCommentsModel>($" select * from [dbo].[tblManagementPlanComments] where [ManagementPlanID] = {PlanID}" , null,

            commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetManagementPlanCommentsAll))]
        public Task<List<ManagementPlanCommentsModel>> GetManagementPlanCommentsAll()
        {
            //var result = Task.FromResult(_dapper.GetAll<NewsFeedModel>($" SELECT * from [dbo].[tblNewsFeed] where datediff(DAY, [dateGenerated], GETDATE()) <= 5 Order By DateGenerated DESC", null,
            var result = Task.FromResult(_dapper.GetAll<ManagementPlanCommentsModel>($" select * from [dbo].[tblManagementPlanComments]", null,

            commandType: CommandType.Text));
            return result;
        }

    }
}
