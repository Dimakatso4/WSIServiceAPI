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
    public class DistrictPositionController : ControllerBase
    {
        private readonly IDapper _dapper;
        public DistrictPositionController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(GetDistrictPosition))]
        public Task<List<DistrictPositionModel>> GetDistrictPosition()
        {
            var Output = Task.FromResult(_dapper.GetAll<DistrictPositionModel>($"select *from[dbo].[tblDistrictRoles]", null,
                    commandType: CommandType.Text));
            return Output;
        }
    }
}
