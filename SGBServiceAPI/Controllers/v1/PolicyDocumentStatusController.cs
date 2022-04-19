using Microsoft.AspNetCore.Http;
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
    public class PolicyDocumentStatusController : ControllerBase
    {
        private readonly IDapper _dapper;
        public PolicyDocumentStatusController(IDapper dapper)
        {
            _dapper = dapper;
        }
        //Get document status
        [HttpGet(nameof(GetDococumentStatusList))]
        public Task<List<PolicyDocumentStatusModel>> GetDococumentStatusList()
        {
            var result = Task.FromResult(_dapper.GetAll<PolicyDocumentStatusModel>($"SELECT * FROM [dbo].[tblDocumentStatus]", null,
            commandType: CommandType.Text));
            return result;
        }



    }
}
