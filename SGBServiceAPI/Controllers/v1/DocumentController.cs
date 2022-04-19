using Microsoft.AspNetCore.Mvc;
using WSIServiceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using WSIServiceAPI.Models;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DocumentController: ControllerBase
    {
        //mnqobi
        private readonly IDapper _dapper; 
        public DocumentController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public async Task<int> Create(DocumentModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", data.DocumentId, DbType.Int32);
            dbparams.Add("@DocumentNumber", data.DocumentNumber);
            dbparams.Add("@DocumentName", data.DocumentName);
            dbparams.Add("@DocumentDescription", data.DocumentDescription);
            dbparams.Add("@AreaOfEvaluationID", data.AreaOfEvaluationID);
            dbparams.Add("@DocumentSavedBy", data.DocumentSavedBy);
            dbparams.Add("@ContactUserID", data.UserID);
            dbparams.Add("@Status", data.Status);
            dbparams.Add("@ApprovedBy", data.ApprovedBy);
            if (data.DateApproved.HasValue)
                dbparams.Add("@DateApproved", data.DateApproved);
            else
                dbparams.Add("@DateApproved", null);
            dbparams.Add("@DateApproved", data.DateApproved);
            if (data.DateLastAmended.HasValue)
                dbparams.Add("@DateLastAmended", data.DateLastAmended);
            else
                dbparams.Add("@DateLastAmended", null);
            if (data.DateNextReview.HasValue)
            dbparams.Add("@DateNextReview", data.DateNextReview);
            else
                dbparams.Add("@DateNextReview", null);
            dbparams.Add("@RelatedPolicies", data.RelatedPolicies);
            dbparams.Add("@documentPath", data.documentPath);
            dbparams.Add("@IsActive", 1);
            //dbparams.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddDocumentLibrary]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@DocumentId");
            return retVal;

        }
        

        [HttpGet(nameof(GetDocumentName))]
        public async Task<List<DocumentModel>> GetDocumentName()
        {
            var result = await Task.FromResult(_dapper.GetAll<DocumentModel>($"select [DocumentName] from [dbo].[DocumentLibrary]", null, commandType: CommandType.Text));
            return result;
        }
        
        [HttpGet(nameof(GetDocumentList))]
        public async Task<List<DocumentModel>> GetDocumentList()
        {
            var result = await Task.FromResult(_dapper.GetAll<DocumentModel>($"Select doc.*, area.FocusArea, u.UserId, u.FirstName + ' ' + u.Surname as 'FullName', u.Email from [dbo].[DocumentLibrary] AS doc Inner join [dbo].[tblManageAreaOfEvaluation] area on area.ManageAreaOfEvalutionID = doc.AreaOfEvaluationID INNER JOIN [dbo].[tblUsers] u on u.UserId = doc.UserID where doc.IsCurrent = 1 and doc.IsActive = 1 ORDER BY doc.[DateLastAmended] DESC", null, commandType: CommandType.Text));
            return result;
        }

        //function to return only signed documents
        [HttpGet(nameof(GetDocumentListSigned))]
        public async Task<List<DocumentModel>> GetDocumentListSigned()
        {
            var result = await Task.FromResult(_dapper.GetAll<DocumentModel>($"Select doc.*, area.FocusArea, u.UserId, u.FirstName + ' ' + u.Surname as 'FullName', u.Email from [dbo].[DocumentLibrary] AS doc Inner join [dbo].[tblManageAreaOfEvaluation] area on area.ManageAreaOfEvalutionID = doc.AreaOfEvaluationID INNER JOIN [dbo].[tblUsers] u on u.UserId = doc.UserID where doc.IsCurrent = 1 and doc.IsActive = 1 and doc.Status='SignedOff' ORDER BY doc.[DateLastAmended] DESC", null, commandType: CommandType.Text));
            return result;
        }


        [HttpGet(nameof(GetDocumentHistoryList))]
        public async Task<List<DocumentModel>> GetDocumentHistoryList(int AreaOfEvalutionID)
        {
            var result = await Task.FromResult(_dapper.GetAll<DocumentModel>($"Select doc.*, area.FocusArea, u.UserId, u.FirstName + ' ' + u.Surname as 'FullName', u.Email from [dbo].[DocumentLibrary] AS doc Inner join [dbo].[tblManageAreaOfEvaluation] area on area.ManageAreaOfEvalutionID = doc.AreaOfEvaluationID INNER JOIN [dbo].[tblUsers] u on u.UserId = doc.UserID where doc.[AreaOfEvaluationID] = {AreaOfEvalutionID} and doc.IsCurrent = 0 ORDER BY doc.[DateLastAmended] DESC", null, commandType: CommandType.Text));
            return result;
        }


        [HttpPatch(nameof(UpdateDocument))]
        public Task<int> UpdateDocument(DocumentModel data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("@DocumentId", data.DocumentId);
            dbPara.Add("@IsActive", data.IsActive);

            var updateDocument = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateDocument]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateDocument;
        }

        [HttpGet(nameof(GetDocumentByStatus))]
        public async Task<List<DocumentModel>> GetDocumentByStatus(int StatusID)
        {
            var result = await Task.FromResult(_dapper.GetAll<DocumentModel>($"select * from [dbo].[DocumentLibrary] where [IsActive] = {StatusID}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPost(nameof(DeleteDocument))]
        public Task<int> DeleteDocument(DocumentModel data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("@DocumentId", data.DocumentId);

            var updateDocument = Task.FromResult(_dapper.Update<int>("[dbo].[spDeleteDocument]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateDocument;
        }



    }
}
