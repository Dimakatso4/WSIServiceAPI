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
    public class HeadOfficePositionController : ControllerBase
    {
        private readonly IDapper _dapper;
        public HeadOfficePositionController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(GetHeadOfficePosition))]
        public Task<List<HeadOfficePositionModel>> GetHeadOfficePosition()
        {
            var Output = Task.FromResult(_dapper.GetAll<HeadOfficePositionModel>($"select *from[dbo].[tblBranchesData]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetBranchesByPositions))]
        public Task<List<HeadOfficePositionModel>> GetBranchesByPositions()
        {
            var Output = Task.FromResult(_dapper.GetAll<HeadOfficePositionModel>($"select *from[dbo].[tblBranchesData]", null,
                    commandType: CommandType.Text));
            return Output;
        }
    }
}
