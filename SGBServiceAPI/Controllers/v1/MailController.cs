using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSIServiceAPI.Models;
using WSIServiceAPI.Services;
using System;
using System.Threading.Tasks;
using System.Data;

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IDapper _dapper;
        private readonly IMailService mailService;
        public MailController(IDapper dapper, IMailService mailService)
        {
            this._dapper = dapper;
            this.mailService = mailService;
        }



        [HttpPost(nameof(SendOTPEmail))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SendOTPEmail(string UserName, string Pass)
        {


            await mailService.SendOTPEmail(UserName, Pass);
            return Ok();

        }

        [HttpPost(nameof(SendPasswordComfirmationEmail))]
        public async Task<IActionResult> SendPasswordComfirmationEmail(String UserName, String ToAddr, String Message)
        {

            // var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from tblUsers where IDNumber={UserName} OR Persal={UserName}", null, commandType: System.Data.CommandType.Text));

            await mailService.SendPasswordComfirmationEmail(UserName,ToAddr,Message);

            return Ok();

        }

        [HttpPost(nameof(SendBulkEmail))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SendBulkEmail(SendEmailModel sendEmailModel)
        {

            try
            {

                if (sendEmailModel.ReceiversList.Length > 0)
                {
                    //string inviteesEmail = string.Join(",", emailInviteeModel.Invitees);

                    MimeKit.InternetAddressList receiverMailBoxeslist = new MimeKit.InternetAddressList();
                    foreach (string inviteeEmail in sendEmailModel.ReceiversList)
                    {
                        if (inviteeEmail.Contains("@"))
                            receiverMailBoxeslist.Add(MimeKit.MailboxAddress.Parse(inviteeEmail));
                    }

                    await mailService.SendEmailBatchAsync(receiverMailBoxeslist, sendEmailModel.Subject, sendEmailModel.Body, sendEmailModel.DocumentName);

                }

                return Ok();
            }
            catch (Exception ex)
            {

                return Ok(ex.StackTrace);
            }

        }

        //[HttpPost(nameof(SendEmail))]
        //public async Task<IActionResult> SendEmail(String UserName)
        //{

        //    // var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from tblUsers where IDNumber={UserName} OR Persal={UserName}", null, commandType: System.Data.CommandType.Text));

        //    await mailService.SendPasswordComfirmationEmail(UserName);

        //    return Ok();

        //}

        //[HttpPost(nameof(SendEmailInvite))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        //public async Task<IActionResult> SendEmailInvite(EmailInviteeModel emailInviteeModel)
        //{
        //    try
        //    {
        //        if (emailInviteeModel.Invitees.Length > 0)
        //        {
        //            MimeKit.InternetAddressList inviteeMailBoxeslist = new MimeKit.InternetAddressList();
        //            foreach (string inviteeEmail in emailInviteeModel.Invitees)
        //            {
        //                if (inviteeEmail.Contains("@"))
        //                {
        //                    inviteeMailBoxeslist.Add(new MimeKit.MailboxAddress(inviteeEmail));
        //                }
        //            }
        //            await mailService.SendEmailBatchAsync(inviteeMailBoxeslist, emailInviteeModel.Subject, emailInviteeModel.Body);
        //        }
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {

        //        return Ok(ex.StackTrace);
        //    }
        //}

        /* [HttpPost(nameof(SendEmailInvite))]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
         public async Task<IActionResult> SendEmailInvite(EmailInviteeModel emailInviteeModel)
         {
             try
             {
                 if (emailInviteeModel.Invitees.Length > 0)
                 {
                     MimeKit.InternetAddressList inviteeMailBoxeslist = new MimeKit.InternetAddressList();
                     foreach (string inviteeEmail in emailInviteeModel.Invitees)
                     {
                         if (inviteeEmail.Contains("@"))
                         {
                             inviteeMailBoxeslist.Add(new MimeKit.MailboxAddress(inviteeEmail));
                         }
                     }
                     await mailService.SendEmailBatchAsync(inviteeMailBoxeslist, emailInviteeModel.Subject, emailInviteeModel.Body);
                 }
                 return Ok();
             }
             catch (Exception ex)
             {

                 return Ok(ex.StackTrace);
             }
         }

         [HttpPost(nameof(SendResetMail))]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
         public async Task<IActionResult> SendResetMail(string Hostname, string UserName, string Id)
         {
             try
             {
                 var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from [tblUsers] where Id={Id}", null, commandType: System.Data.CommandType.Text));
                 await mailService.SendResetEmailAsync(UserName, Hostname, result.Email, result.Password, Id);
                 return Ok();
             }
             catch (Exception ex)
             {

                 throw;
             }
         }*/

        ////Mnqobi
        [HttpPost(nameof(SendEmail))]
        public async Task<IActionResult> SendEmail(string hostname,int Id, string Password)
        {



             var result = await Task.FromResult(_dapper.Get<UsersModel>($"select * from [dbo].[tblUsers] where UserId = {Id}", null, commandType: System.Data.CommandType.Text));


            await mailService.SendEmail(hostname,Id, Password);




            return Ok();


        }

        ////Mnqobi
        [HttpPost(nameof(SendGenericEmail))]
        public async Task<IActionResult> SendGenericEmail(string Subject, string ToAddress, string Body)
        {

            await mailService.SendGenericEmail(ToAddress, Body, Subject);

            return Ok();

        }

        ////Mnqobi
        [HttpPost(nameof(SendGenericEmailBulk))]
        public async Task<IActionResult> SendGenericEmailBulk(string Subject, string[] ToAddress, string Body)
        {

            if (ToAddress.Length > 0)
            {
                MimeKit.InternetAddressList inviteeMailBoxeslist = new MimeKit.InternetAddressList();
                foreach (string emailAdd in ToAddress)
                {
                    if (emailAdd.Contains("@"))
                    {
                        inviteeMailBoxeslist.Add(MimeKit.MailboxAddress.Parse(emailAdd));
                    }
                }
                await mailService.SendGenericEmailBatchAsync(inviteeMailBoxeslist, Subject, Body);
            }

            return Ok();


        }

        //Mnqobi Documentemail

        [HttpPost(nameof(SendEmailDocumentUpload))]
        public async Task<IActionResult> SendEmailDocumentUpload(string UserName)
        {



             var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select UserName, Email from tblUsers where UserName='{UserName}'", null, commandType: System.Data.CommandType.Text));

            if (result == null) return Ok("Email not send");

            if (!string.IsNullOrEmpty(result.Email))
                { 
                await mailService.SendDocumentUploadEmail(UserName, result.Email);
                return Ok();

                }
                else {
                return Ok("Email not send");
            }

        

        }
        [HttpPost(nameof(DocumentEmail))]
        public async Task<IActionResult> DocumentEmail(string UserName)
        {



            // var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from tblUsers where IDNumber={UserName} OR Persal={UserName}", null, commandType: System.Data.CommandType.Text));



            await mailService.DocumentEmail(UserName);



            return Ok();


        }

        //Mnqobi reset email
        //Send a user reset password link 
        [HttpPost(nameof(SendResetMail))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SendResetMail(string UserName, string Id)
        {
            try
            {

                var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from [tblUsers] where UserId = {Id}", null, commandType: CommandType.Text));

                await mailService.SendResetEmailAsync(UserName,result.Email, result.Password, Id);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        //Mnqobi updated by Nkosinathi
        //[HttpPost(nameof(SendEmail))]
        //public async Task<string> SendEmail(string toMail, string firstname, string persal)
        //{



        //    // var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from tblUsers where IDNumber={UserName} OR Persal={UserName}", null, commandType: System.Data.CommandType.Text));



        //    var pass = await mailService.SendEmail(toMail, firstname, persal);



        //    return pass;


        //}
    }
}
