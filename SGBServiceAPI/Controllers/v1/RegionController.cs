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
    public class RegionController : ControllerBase
    {
        private readonly IDapper _dapper;
        public RegionController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(GetRegionNames))]
        public Task<List<RegionModel>> GetRegionNames()
        {
            var Output = Task.FromResult(_dapper.GetAll<RegionModel>($"select distinct Region from dbo.tblDistrictWSI", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetDistrictWSI))]
        public Task<List<RegionModel>> GetDistrictWSI()
        {
            var Output = Task.FromResult(_dapper.GetAll<RegionModel>($"select *from [dbo].[tblDistrictWSI]", null,
                    commandType: CommandType.Text));
            return Output;
        }
    }
}
