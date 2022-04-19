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
    public class KPICommentsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public KPICommentsController(IDapper dapper)
        {
            _dapper = dapper;

        }
        [HttpPost(nameof(CreateKPIComments))]
        public async Task<int> CreateKPIComments(KPICommentsModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SchoolKPIID", data.SchoolKPIID, DbType.Int32);
            dbparams.Add("@UserId", data.UserID, DbType.Int32);
            dbparams.Add("@Comments", data.Comment);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SPAddKPIComments]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@CommentID");
            return retVal;
        }

        [HttpGet(nameof(GetKPICommentsAll))]
        public Task<List<KPICommentsModel>> GetKPICommentsAll()
        {
            var ArealOfEvaluationResult = Task.FromResult(_dapper.GetAll<KPICommentsModel>($"Select * from [dbo].[tblKPIComments]", null,
            commandType: CommandType.Text));



            return ArealOfEvaluationResult;
        }

        [HttpGet(nameof(GetKPICommentsBySchoolKPI))]
        public Task<List<KPICommentsModel>> GetKPICommentsBySchoolKPI(int KPIID)
        {
            var ArealOfEvaluationResult = Task.FromResult(_dapper.GetAll<KPICommentsModel>($"Select * from [dbo].[tblKPIComments] where [SchoolKPIID] = {KPIID}", null,
            commandType: CommandType.Text));



            return ArealOfEvaluationResult;
        }

        [HttpGet(nameof(GetKPICommentsByUser))]
        public Task<List<KPICommentsModel>> GetKPICommentsByUser(int UserID)
        {
            var ArealOfEvaluationResult = Task.FromResult(_dapper.GetAll<KPICommentsModel>($"Select * from [dbo].[tblKPIComments] where [UserId] = {UserID}", null,
            commandType: CommandType.Text));



            return ArealOfEvaluationResult;
        }

    }
}
