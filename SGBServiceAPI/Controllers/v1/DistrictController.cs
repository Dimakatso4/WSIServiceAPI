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
    public class DistrictController : ControllerBase
    {
        private readonly IDapper _dapper;
        public DistrictController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateDistrict))]
        public async Task<int> CreateDistrict(DistrictModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@DistrictId", data.DistrictId, DbType.Int32);
            dataBaseParams.Add("@DistrictName", data.DistrictName);
            dataBaseParams.Add("@DistrictCode", data.DistrictCode);
            dataBaseParams.Add("@Province", data.Province);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddDistrict]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("DistrictId");
            return returnVal;
        }
        [HttpGet(nameof(GetDistrictList))]
        public Task<List<DistrictModel>> GetDistrictList()
        {
            var Output = Task.FromResult(_dapper.GetAll<DistrictModel>($"select * from [dbo].[tblDistrict]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetDistrictNameByDistrictCode))]
        public Task<DistrictModel> GetDistrictNameByDistrictCode(string DistrictCode)
        {
            var Output = Task.FromResult(_dapper.Get<DistrictModel>($"select [DistrictId], [DistrictName], [DistrictCode], [Province] from [dbo].[tblDistrict] where [DistrictCode] = '{DistrictCode}'", null,
                    commandType: CommandType.Text));
            return Output;
        }


        

        [HttpGet(nameof(GetDistrictById))]
        public Task<List<DistrictModel>> GetDistrictById(int Id)
        {
            var Output = Task.FromResult(_dapper.GetAll<DistrictModel>($"select * from [dbo].[tblDistrict] where DistrictId={Id}", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpPatch(nameof(updateDistrict))]
        public Task<int> updateDistrict(DistrictModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@DistrictId", data.DistrictId, DbType.Int32);
            dataBaseParams.Add("@DistrictName", data.DistrictName);
            dataBaseParams.Add("@DistrictCode", data.DistrictCode);
            dataBaseParams.Add("@Province", data.Province);

            var updateDistrict = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateDistrict]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateDistrict;

        }

        [HttpGet(nameof(GetDistrictNames))]
        public Task<List<DistrictModel>> GetDistrictNames()
        {
            var Output = Task.FromResult(_dapper.GetAll<DistrictModel>($"select [DistrictName] from [dbo].[tblDistrict]", null,
                    commandType: CommandType.Text));
            return Output;
        }


    }
}
