using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WSIServiceAPI.Models;
using WSIServiceAPI.Services;

namespace WSIServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompleteSSEController : ControllerBase
    {
        private readonly IDapper _dapper;
        public CompleteSSEController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateCompleteSSE))]
        public async Task<int> CreateCompleteSSE(CompleteSSEModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CompleteSSEID", DbType.Int32);
            dbparams.Add("@SseQuestionID", DbType.Int32);
            dbparams.Add("@Rate", data.Rate);
            dbparams.Add("@CompletedDate", data.CompletedDate);
            dbparams.Add("@EvidenceDescription", data.EvidenceDescription);
            dbparams.Add("@PrincipalComment", data.PrincipalComment);
            dbparams.Add("@SGBComment", data.SGBComment);
            dbparams.Add("@BUAssigned", data.BUAssigned);
            dbparams.Add("@BUCompleteDate", data.BUCompleteDate);
            dbparams.Add("@BUComment", data.BUComment);
            dbparams.Add("@BUSuggestion", data.BUSuggestion);
            dbparams.Add("@BUstatus", data.BUstatus);
            dbparams.Add("@Status", data.Status);
            dbparams.Add("@SchoolId", DbType.Int32); var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddCompleteSSE]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@CompleteSSEID");
            return retVal;
        }
        [HttpGet(nameof(GetCompleteSSE))]
        public Task<List<CompleteSSEModel>> GetCompleteSSE()
        {
            var result = Task.FromResult(_dapper.GetAll<CompleteSSEModel>($"SELECT * FROM [dbo].[tblSSEQuestions] Q, [dbo].[tblCompleteSSE] C WHERE C.[SseQuestionID] = Q.[SseQuestionID]", null,
            commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetCompleteSSEById))]
        public Task<List<CompleteSSEModel>> GetCompleteSSEById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<CompleteSSEModel>($"select * from [tblCompleteSSE] where CompleteSSEID={Id}", null,
            commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetSchoolById))]
        public Task<List<CompleteSSEModel>> GetSchoolById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<CompleteSSEModel>($"select * from [tblCompleteSSE] where SchoolId={Id}", null,
            commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetKPIQuestionById))]
        public Task<List<CompleteSSEModel>> GetKPIQuestionById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<CompleteSSEModel>($"select * from [tblCompleteSSE] where SseQuestionID={Id}", null,
            commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdateCompleteSSE))]
        public Task<int> UpdateCompleteSSE(CompleteSSEModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CompleteSSEID", data.CompleteSSEID, DbType.Int32);
            dbparams.Add("@SseQuestionID", data.SseQuestionID, DbType.Int32);
            dbparams.Add("@Rate", data.Rate);
            dbparams.Add("@CompletedDate", data.CompletedDate);
            dbparams.Add("@EvidenceDescription", data.EvidenceDescription);
            dbparams.Add("@PrincipalComment", data.PrincipalComment);
            dbparams.Add("@SGBComment", data.SGBComment);
            dbparams.Add("@BUAssigned", data.BUAssigned);
            dbparams.Add("@BUCompleteDate", data.BUCompleteDate);
            dbparams.Add("@BUComment", data.BUComment);
            dbparams.Add("@BUSuggestion", data.BUSuggestion);
            dbparams.Add("@BUstatus", data.BUstatus);
            dbparams.Add("@Status", data.Status);
            dbparams.Add("@SchoolId", data.SchoolId, DbType.Int32); var updateCompleteSSE = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateCompleteSSE]",
            dbparams,
            commandType: CommandType.StoredProcedure));
            return updateCompleteSSE;
        }

        [HttpPatch(nameof(UpdateBUCompleteSSE))]
        public Task<int> UpdateBUCompleteSSE(CompleteSSEModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CompleteSSEID", data.CompleteSSEID, DbType.Int32);
            dbparams.Add("@BUAssigned", data.BUAssigned);
            dbparams.Add("@BUCompleteDate", data.BUCompleteDate);
            dbparams.Add("@BUstatus", data.BUstatus);
            var updateBUCompleteSSE = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateBUCompleteSSE]",
            dbparams,
            commandType: CommandType.StoredProcedure));
            return updateBUCompleteSSE;
        }

        [HttpPatch(nameof(UpdateReviewCompleteSSE))]
        public Task<int> UpdateReviewCompleteSSE(CompleteSSEModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CompleteSSEID", data.CompleteSSEID, DbType.Int32);
            dbparams.Add("@BUComment", data.BUComment);
            dbparams.Add("@BUSuggestion", data.BUSuggestion);
            dbparams.Add("@BUstatus", data.BUstatus);
            dbparams.Add("@Status", data.Status);
            var updateReviewCompleteSSE = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateReviewCompleteSSE]",
            dbparams,
            commandType: CommandType.StoredProcedure));
            return updateReviewCompleteSSE;
        }
    }
}
