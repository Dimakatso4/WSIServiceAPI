
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
    public class QuarterController : ControllerBase
    {
        private readonly IDapper _dapper;
        public QuarterController(IDapper dapper)
        {
            _dapper = dapper;
        }


        [HttpGet(nameof(GetQuarterByMonth))]
        public Task<int> GetQuarterByMonth(string Month)
        {
            var result = Task.FromResult(_dapper.Get<int>($"select Quarter from [tblQuarter] Where Month='{Month}'", null,
                    commandType: CommandType.Text));
            return result;
        }
    }
}
