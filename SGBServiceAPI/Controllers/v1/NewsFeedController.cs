using Dapper;
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
    public class NewsFeedController : ControllerBase
    {
        private readonly IDapper _dapper;

        public NewsFeedController(IDapper dapper)
        {
            _dapper = dapper;

        }

        [HttpPost(nameof(CreateNewsFeed))]
        public async Task<int> CreateNewsFeed(NewsFeedModel data)
        {

            var dbparams = new DynamicParameters();
            dbparams.Add("@NewsFeedID", data.newsFeedID, DbType.Int32);
            dbparams.Add("@title", data.title);
            dbparams.Add("@author", data.author);
            dbparams.Add("@message", data.message);
            dbparams.Add("@newsFeedType", data.newsFeedType, DbType.Int32);
            dbparams.Add("@dateGenerated", DateTime.Now);
            dbparams.Add("@startDate", data.startDate);
            dbparams.Add("@endDate", data.endDate);
            dbparams.Add("@newsFeedImages", data.newsFeedImages);
            dbparams.Add("@StartDate", data.startDate, DbType.Date);
            dbparams.Add("@EndDate", data.endDate, DbType.Date);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddNewsFeed]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@NewsFeedID");
            return retVal;
        }
        [HttpPost(nameof(UpdateNewsFeed))]
        public Task<int> UpdateNewsFeed(NewsFeedModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@NewsFeedID", data.newsFeedID, DbType.Int32);
            dbparams.Add("@title", data.title);
            dbparams.Add("@author", data.author);
            dbparams.Add("@message", data.message);
            dbparams.Add("@newsFeedType", data.newsFeedType, DbType.Int32);
            dbparams.Add("@newsFeedImages", data.newsFeedImages);
            dbparams.Add("@StartDate", data.startDate, DbType.Date);
            dbparams.Add("@EndDate", data.endDate, DbType.Date);


            var updateU = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateNewsFeed]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateU;
        }

        [HttpGet(nameof(GetNewsType))]
        public async Task<List<NewsFeedTypeModel>> GetNewsType()
        {
            var result = await Task.FromResult(_dapper.GetAll<NewsFeedTypeModel>($"SELECT * FROM [dbo].[tblNewsFeedType]", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetNewsFeedListAll))]
        public Task<List<NewsFeedModel>> GetNewsFeedListAll()
        {
        //var result = Task.FromResult(_dapper.GetAll<NewsFeedModel>($" SELECT * from [dbo].[tblNewsFeed] where datediff(DAY, [dateGenerated], GETDATE()) <= 5 Order By DateGenerated DESC", null,
        var result = Task.FromResult(_dapper.GetAll<NewsFeedModel>($"SELECT nf.*, nft.description from [dbo].[tblNewsFeed] nf INNER JOIN [dbo].[tblNewsFeedType] nft ON nft.newsFeedTypeID = nf.NewsFeedType where nf.EndDate >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) and IsActive = 1 Order By nf.DateGenerated DESC", null,

        commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetNewsFeedListByTypeID))]
        public Task<List<NewsFeedModel>> GetNewsFeedListByTypeID(int TypeID)
        {
            //var result = Task.FromResult(_dapper.GetAll<NewsFeedModel>($" SELECT * from [dbo].[tblNewsFeed] where datediff(DAY, [dateGenerated], GETDATE()) <= 5 Order By DateGenerated DESC", null,
            var result = Task.FromResult(_dapper.GetAll<NewsFeedModel>($"SELECT nf.*, nft.description from [dbo].[tblNewsFeed] nf INNER JOIN [dbo].[tblNewsFeedType] nft ON nft.newsFeedTypeID = nf.NewsFeedType where nf.NewsFeedType = {TypeID} and EndDate >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) and IsActive = 1 Order By nf.DateGenerated DESC", null,

            commandType: CommandType.Text));
            return result;
        }



        [HttpGet(nameof(GetNewsFeedById))]
        public Task<List<NewsFeedModel>> GetNewsFeedById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<NewsFeedModel>($"SELECT nf.*, nft.description from [dbo].[tblNewsFeed] nf INNER JOIN [dbo].[tblNewsFeedType] nft ON nft.newsFeedTypeID = nf.NewsFeedType where nf.[newsFeedID]={Id}", null,
                commandType: CommandType.Text));
            return result;
        }


        [HttpPatch(nameof(DeleteNewsFeed))]
        public Task<int> DeleteNewsFeed(NewsFeedModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@newsFeedID", data.newsFeedID, DbType.Int32);

            var updateU = Task.FromResult(_dapper.Update<int>("[dbo].[spDeleteNewsFeed]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateU;
        }
    }
}
