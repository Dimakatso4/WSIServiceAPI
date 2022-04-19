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
using System.Net.Http;

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        /*private readonly IDapper _dapper;
        public NotificationController(IDapper dapper)
        {
            _dapper = dapper;
        }

        private async void SendSMS(string phoneNumber, string Message)
        {
            var client = new HttpClient();
            await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://www.xml2sms.gsm.co.za/send?username=anisadefreitas&password=education15&number=" + phoneNumber + "&message=" + Message));

        }

        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;

            }
            return sOTP;

        }

        private string GenerateOTPNumber()
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            string randomOTP = GenerateRandomOTP(4, saAllowedCharacters);

            return randomOTP;
        }

        [HttpPost(nameof(SendOTP))]
        public async Task<string> SendOTP(int userId)
        {
            string randomOTP = GenerateOTPNumber();

            var userPhoneRequest = Task.FromResult(_dapper.Get<NotificationModel>($"SELECT DISTINCT [Cell] FROM [dbo].[tblUsers] WHERE [UserId] = '{userId}'", null,
                    commandType: CommandType.Text));

            string userPhoneNumber = userPhoneRequest.Result.PhoneNumber.ToString();

            var dbparams = new DynamicParameters();
            dbparams.Add("@OtpNumber", randomOTP);
            dbparams.Add("@PhoneNumber", userPhoneNumber, DbType.Int32);
            dbparams.Add("@OtpTimeExpiry", DateTime.Now.AddHours(4));
            dbparams.Add("@Message", String.Format("Never share this OTP with anyone:{0} Queries? 0861004000. Sent {1}", randomOTP, DateTime.Now.ToString()));
            dbparams.Add("@IsActive", true);
            dbparams.Add("@NotificationID", 0, DbType.Int32, ParameterDirection.Output);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_SendOTP]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@NotificationID");

            SendSMS(userPhoneNumber, String.Format("Never share this OTP with anyone:{0} Queries? 0861004000. Sent {1}", randomOTP, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));

            return userPhoneNumber;
        }

        [HttpPost(nameof(AuthenticateOTP))]
        public async Task<bool> AuthenticateOTP(string PhoneNumber, string OTP)
        {
            var currentDate = DateTime.Now;
            var result = await Task.FromResult(_dapper.Get<NotificationModel>($"Select [NotificationID] Id,[OtpNumber],[PhoneNumber],[OtpTimeCreated],[OtpTimeExpiry],[IsActive],[Message] from [dbo].[tblNotifications] where PhoneNumber = {PhoneNumber} AND OtpNumber = {OTP} AND OtpTimeExpiry>'{currentDate}' AND IsActive = 1", null,
                    commandType: CommandType.Text));
            
            if (result != null)
            {
               //Update the OTP status to prevent reuse
               await Task.FromResult(_dapper.Get<NotificationModel>($"UPDATE [dbo].[tblNotifications] SET IsActive = 0 WHERE NotificationID = {result.NotificationID}", null,
                    commandType: CommandType.Text));
                return true;
            }

            return false;
        }
         
        [HttpGet(nameof(ResendOTP))]
        public bool ResendOTP(int Id)
        {
            var smsmessage = Task.FromResult(_dapper.Get<NotificationModel>($"Select * from [dbo].[tblNotifications] where NotificationID = {Id} AND IsActive = 1", null,
                    commandType: CommandType.Text));

            //TODO: send sms again
            return true;
        }
        */

    }
}
