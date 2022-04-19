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

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodController : ControllerBase
    {
        private readonly IDapper _dapper;
        public PeriodController(IDapper dapper)
        {
            _dapper = dapper;
        }


        [HttpGet(nameof(GetPeriodAll))]
        public Task<List<PeriodModel>> GetPeriodAll()
        {
            var Output = Task.FromResult(_dapper.GetAll<PeriodModel>($"select * from tblManagementPeriod", null,
                    commandType: CommandType.Text));
            return Output;
        }

  
    }
}
