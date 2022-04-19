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
    public class CircuitController : ControllerBase
    {

        private readonly IDapper _dapper;
        public CircuitController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(GetCircuitNo))]
        public Task<List<CircuitModel>> GetCircuitNo()
        {
            var Output = Task.FromResult(_dapper.GetAll<CircuitModel>($"select distinct CircuitNo,DistrictName  from  [dbo].[GDEMainData]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetUniqueCircuitNo))]
        public Task<List<CircuitModel>> GetUniqueCircuitNo()
        {
            var Output = Task.FromResult(_dapper.GetAll<CircuitModel>($"select distinct CircuitNo  from  [dbo].[GDEMainData]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetClusterNo))]
        public Task<List<ClusterNoModel>> GetClusterNo()
        {
            var Output = Task.FromResult(_dapper.GetAll<ClusterNoModel>($"Select distinct CircuitNo,   ClusterNo    from [dbo].[GDEMainData]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetClusterUniqueNo))]
        public Task<List<ClusterNoModel>> GetClusterUniqueNo()
        {
            var Output = Task.FromResult(_dapper.GetAll<ClusterNoModel>($"Select distinct    ClusterNo    from [dbo].[GDEMainData]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetSchoolNames))]
        public Task<List<SchoolNamesModel>> GetSchoolNames()
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolNamesModel>($"Select  InstitutionName ,ClusterNo  from  [dbo].[GDEMainData]", null,
                    commandType: CommandType.Text));
            return Output;
        }
    }
}
