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
    public class SchoolTermsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SchoolTermsController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateSchoolTerm))]
        public async Task<int> CreateSchoolTerm(SchoolTermsModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@TermID", data.TermID, DbType.Int32);
            dbparams.Add("@Term1Start", data.Term1Start, DbType.Date);
            dbparams.Add("@Term1End", data.Term1End, DbType.Date);
            dbparams.Add("@Term2Start", data.Term2Start, DbType.Date);
            dbparams.Add("@Term2End", data.Term2End, DbType.Date);
            dbparams.Add("@Term3Start", data.Term3Start, DbType.Date);
            dbparams.Add("@Term3End", data.Term3End, DbType.Date);
            dbparams.Add("@Term4Start", data.Term4Start, DbType.Date);
            dbparams.Add("@Term4End", data.Term4End, DbType.Date);



            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddSchoolTerms]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            var returnVal = dbparams.Get<int>("@TermID");
            return returnVal;
        }
        //[HttpGet(nameof(ViewTermCalendar))]
        //public Task<List<SchoolTermsModel>> ViewTermCalendar(int TermID)
        //{
        // var Output = Task.FromResult(_dapper.GetAll<SchoolTermsModel>($"SELECT [Term1Start],[Term1End],[Term2Start],[Term2End],[Term3Start],[Term3End],[Term4Start],[Term4End] FROM [dbo].[tblSchoolTerms] Where TermID = {TermID}", null,
        // commandType: CommandType.Text));
        // return Output;
        //}
        [HttpGet(nameof(ListTermCalendar))]
        public Task<List<SchoolTermsModel>> ListTermCalendar()
        {
            var Output = Task.FromResult(_dapper.GetAll<SchoolTermsModel>($"SELECT * FROM [dbo].[tblSchoolTerms] Order by Term1Start DESC", null,
            commandType: CommandType.Text));
            return Output;
        }
        [HttpPatch(nameof(UpdateSchoolTerm))]
        public Task<int> UpdateSchoolTerm(SchoolTermsModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@TermID", data.TermID, DbType.Int32);
            dbparams.Add("@Term1Start", data.Term1Start, DbType.Date);
            dbparams.Add("@Term1End", data.Term1End, DbType.Date);
            dbparams.Add("@Term2Start", data.Term2Start, DbType.Date);
            dbparams.Add("@Term2End", data.Term2End, DbType.Date);
            dbparams.Add("@Term3Start", data.Term3Start, DbType.Date);
            dbparams.Add("@Term3End", data.Term3End, DbType.Date);
            dbparams.Add("@Term4Start", data.Term4Start, DbType.Date);
            dbparams.Add("@Term4End", data.Term4End, DbType.Date);



            var updateST = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSchoolTerms]",
            dbparams,
            commandType: CommandType.StoredProcedure));
            return updateST;
        }
        [HttpPatch(nameof(UpdateSchoolTermByID))]
        public Task<int> UpdateSchoolTermByID(SchoolTermsModel data,int ID)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@TermID",ID, DbType.Int32);
            dbparams.Add("@Term1Start", data.Term1Start, DbType.Date);
            dbparams.Add("@Term1End", data.Term1End, DbType.Date);
            dbparams.Add("@Term2Start", data.Term2Start, DbType.Date);
            dbparams.Add("@Term2End", data.Term2End, DbType.Date);
            dbparams.Add("@Term3Start", data.Term3Start, DbType.Date);
            dbparams.Add("@Term3End", data.Term3End, DbType.Date);
            dbparams.Add("@Term4Start", data.Term4Start, DbType.Date);
            dbparams.Add("@Term4End", data.Term4End, DbType.Date);



            var updateAP = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSchoolTerms]",
            dbparams,
            commandType: CommandType.StoredProcedure));
            return updateAP;
        }
        [HttpGet(nameof(GetSchoolTermByID))]
        public Task<List<SchoolTermsModel>> GetSchoolTermByID(int ID)
        {
            var Term = Task.FromResult(_dapper.GetAll<SchoolTermsModel>($"select [Term1Start],[Term1End],[Term2Start],[Term2End],[Term3Start],[Term3End],[Term4Start],[Term4End] from [dbo].[tblSchoolTerms] where [TermID] = {ID}", null,
            commandType: CommandType.Text));
            return Term;
        }
    }
}