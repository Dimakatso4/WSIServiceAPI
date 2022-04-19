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

namespace WSIServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementPlanStatusController : ControllerBase
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        private readonly IDapper _dapper;
        public ManagementPlanStatusController(IDapper dapper)
        {
            _dapper = dapper;
        }

        /*[HttpPost(nameof(CreateResponsiblePerson))]
        public async Task<int> CreateResponsiblePerson(ManagementPlanStatusModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@StatusID", data.StatusID, DbType.Int32);
            dataBaseParams.Add("@Status", data.Status);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddManagementPlanStatus]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var Status = dataBaseParams.Get<int>("StatusID");
            return Status;
        }*/
        [HttpGet(nameof(GetStatusList))]
        public Task<List<ManagementPlanStatusModel>> GetStatusList()
        {
            var Status = Task.FromResult(_dapper.GetAll<ManagementPlanStatusModel>($"select * from [dbo].[tblManagementPlanStatus]", null,
                    commandType: CommandType.Text));
            return Status;
        }
        [HttpGet(nameof(GetStatusByID))]
        public Task<List<ManagementPlanStatusModel>> GetStatusByID(int ID)
        {
            var Status = Task.FromResult(_dapper.GetAll<ManagementPlanStatusModel>($"select * from [dbo].[tblManagementPlanStatus] where [StatusID] = {ID}", null,
                    commandType: CommandType.Text));
            return Status;
        }

        [HttpGet(nameof(GetStatusByApprovedandUpdate))]
        public Task<List<ManagementPlanStatusModel>> GetStatusByApprovedandUpdate()
        {
            var Status = Task.FromResult(_dapper.GetAll<ManagementPlanStatusModel>($"select * from [dbo].[tblManagementPlanStatus] where [StatusID] = 1 or [StatusID] = 2", null,
                    commandType: CommandType.Text));
            return Status;
        }

    }

}

