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
    public class DipCommentsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public DipCommentsController(IDapper dapper)
        {
            _dapper = dapper;

        }
        [HttpPost(nameof(CreateDipComments))]
        public async Task<int> CreateDipComments(KPICommentsModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SchoolKPIID", data.SchoolKPIID, DbType.Int32);
            dbparams.Add("@UserId", data.UserID, DbType.Int32);
            dbparams.Add("@Comments", data.Comment);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_DipCommentsAdd]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@CommentID");
            return retVal;
        }

        [HttpGet(nameof(GetDipCommentsAll))]
        public Task<List<DipCommentsModel>> GetDipCommentsAll()
        {
            var ArealOfEvaluationResult = Task.FromResult(_dapper.GetAll<DipCommentsModel>($"Select * from [dbo].[tblDipComments]", null,
            commandType: CommandType.Text));



            return ArealOfEvaluationResult;
        }

        [HttpGet(nameof(GetDipCommentsByDipID))]
        public Task<List<DipCommentsModel>> GetDipCommentsByDipID(int DipID)
        {
            var ArealOfEvaluationResult = Task.FromResult(_dapper.GetAll<DipCommentsModel>($"Select * from [dbo].[tblDipComments] where [DipKPIID] = {DipID}", null,
            commandType: CommandType.Text));



            return ArealOfEvaluationResult;
        }

        [HttpGet(nameof(GetDipCommentsByUser))]
        public Task<List<DipCommentsModel>> GetDipCommentsByUser(int UserID)
        {
            var ArealOfEvaluationResult = Task.FromResult(_dapper.GetAll<DipCommentsModel>($"Select * from [dbo].[tblKPIComments] where [UserId] = {UserID}", null,
            commandType: CommandType.Text));



            return ArealOfEvaluationResult;
        }

    }
}
