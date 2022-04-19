
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WSIServiceAPI.Models;
using WSIServiceAPI.Services;

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SIPActionPlanCommentsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SIPActionPlanCommentsController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateSIPActionPlanComments))]
        public async Task<int> CreateSIPActionPlanComments(SIPActionPlanModelComments data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SIPActionPlanId", data.SipActionPlanID, DbType.Int32);
            dbparams.Add("@Status", data.Status);
            dbparams.Add("@Comments", data.Comments);
            dbparams.Add("@Path", data.Path);
            dbparams.Add("@DIPBUComment", data.DIPBUComment);
            dbparams.Add("@DIPCircuitComent", data.DIPCircuitComent);
            dbparams.Add("@DIPDirectComment", data.DIPDirectComment);
            dbparams.Add("@PIPBuComment", data.DIPDirectComment);
            dbparams.Add("@PIPLineDirectorComment", data.DIPDirectComment);
            dbparams.Add("@PIPChiefComment", data.DIPDirectComment);
            dbparams.Add("@PIPDepComment", data.DIPDirectComment);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddSIPActionPlanComments]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@SIPActionPlanId");
            return retVal;
        }

        [HttpPost(nameof(UpdateSIPActionPlanComments))]
        public async Task<int> UpdateSIPActionPlanComments(SIPActionPlanModelComments data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SipActionPlanCommentsID", data.SipActionPlanCommentsID, DbType.Int32);
            dbparams.Add("@Status", data.Status);
            dbparams.Add("@Comments", data.Comments);
            dbparams.Add("@Path", data.Path);
            dbparams.Add("@DIPBUComment", data.DIPBUComment);
            dbparams.Add("@DIPCircuitComent", data.DIPCircuitComent);
            dbparams.Add("@DIPDirectComment", data.DIPDirectComment);
            dbparams.Add("@PIPBuComment", data.DIPDirectComment);
            dbparams.Add("@PIPLineDirectorComment", data.DIPDirectComment);
            dbparams.Add("@PIPChiefComment", data.DIPDirectComment);
            dbparams.Add("@PIPDepComment", data.DIPDirectComment);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spUpdateSIPActionPlanComments]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@SipActionPlanCommentsID");
            return retVal;
        }
  

        [HttpGet(nameof(GetDIPActionPlanComments))]
        public Task<List<SIPActionPlanModelComments>> GetDIPActionPlanComments()
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModelComments>($"select * from [dbo].[tblSipActionPlanComments]", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetDIPActionPlanCommentsByID))]
        public Task<List<SIPActionPlanModelComments>> GetDIPActionPlanCommentsByID(int SipActionPlanCommentsID)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModelComments>($"select * from [dbo].[tblSipActionPlanComments] where SipActionPlanCommentsID =  {SipActionPlanCommentsID}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetDIPActionPlanCommentsByStatus))]
        public Task<List<SIPActionPlanModelComments>> GetDIPActionPlanCommentsByStatus(string Status, int SipActionPlanID)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModelComments>($"select * from [dbo].[tblSipActionPlanComments] where Status =  '{Status}' and SipActionPlanID =  {SipActionPlanID}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetDIPActionPlanCommentsBySipActionPlanID))]
        public Task<List<SIPActionPlanModelComments>> GetDIPActionPlanCommentsBySipActionPlanID(int SipActionPlanID)
        {
            var result = Task.FromResult(_dapper.GetAll<SIPActionPlanModelComments>($"select * from [dbo].[tblSipActionPlanComments] where SipActionPlanID =  {SipActionPlanID}", null, commandType: CommandType.Text));
            return result;
        }

    }

}
