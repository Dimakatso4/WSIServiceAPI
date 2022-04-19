using AutoMapper.Configuration;
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
    public class ManageComponentController : Controller
    {
        //private IConfiguration _config;
        private readonly IDapper _dapper;

        public ManageComponentController(IDapper dapper)
        {
            //_config = config;
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateComponent))]
        public async Task<int> CreateComponent(SSEComponentModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@ManageAreaOfEvaluationID", data.ManageAreaOfEvaluationID, DbType.Int32);
            dataBaseParams.Add("@ComponentName", data.ComponentName, DbType.String);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spComponentAdd]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = 0;
            return returnVal;
        }

        [HttpGet(nameof(GetAllSSEComponent))]
        public Task<List<SSEComponentModel>> GetAllSSEComponent()
        {
            /*var Output = Task.FromResult(_dapper.GetAll<SSEComponentModel>($"select * from [dbo].[tblAreaOfEvaluation]", null,
                    commandType: CommandType.Text));*/

            return Task.FromResult(_dapper.GetAll<SSEComponentModel>($"select mc.*, maov.FocusArea  from tblManageComponents mc Inner JOIN [dbo].[tblManageAreaOfEvaluation] maov on maov.ManageAreaOfEvalutionID = mc.ManageAreaOfEvaluationID", null,
                    commandType: CommandType.Text));

        }
        [HttpGet(nameof(GetSSEComponentByID))]
        public Task<List<SSEComponentModel>> GetSSEComponentByID(int ID)
        {
            var ComponentList = Task.FromResult(_dapper.GetAll<SSEComponentModel>($"select mc.*, maov.FocusArea  from tblManageComponents mc Inner JOIN [dbo].[tblManageAreaOfEvaluation] maov on maov.ManageAreaOfEvalutionID = mc.ManageAreaOfEvaluationID Where mc.ManageComponentID = {ID}", null,
                    commandType: CommandType.Text));
            return ComponentList;
        }

        [HttpGet(nameof(GetSSEComponentByAreaID))]
        public Task<List<SSEComponentModel>> GetSSEComponentByAreaID(int ID)
        {
            var ComponentList = Task.FromResult(_dapper.GetAll<SSEComponentModel>($"select mc.*, maov.FocusArea  from tblManageComponents mc Inner JOIN [dbo].[tblManageAreaOfEvaluation] maov on maov.ManageAreaOfEvalutionID = mc.ManageAreaOfEvaluationID Where maov.ManageAreaOfEvalutionID = {ID}", null,
                    commandType: CommandType.Text));
            return ComponentList;
        }
    }
}
