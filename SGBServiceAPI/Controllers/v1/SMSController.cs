using Microsoft.AspNetCore.Mvc;
using WSIServiceAPI.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using WSIServiceAPI.Models;
using System.Data;
using System.Net.Http;
using System;
using Microsoft.AspNetCore.Http;

namespace WSIServiceAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : Controller
    {

        private readonly IDapper _dapper;
        public string parentMobileNumber;
        public SMSController(IDapper dapper)
        {
            _dapper = dapper;
        }


        [HttpPost(nameof(SendOTP))]
        public async Task<string> SendOTP(string UserName)
        {
            string randomOTP = GenerateOTPNumber();
            //int UserID = 0;
            //int.TryParse(UserId, out UserID);

            var parentMobileRequest = Task.FromResult(_dapper.Get<UsersModel>($"SELECT DISTINCT Cell FROM [dbo].[tblUsers] WHERE [IDNumber]='{UserName}' OR [Persal]='{UserName}' OR [Passport]='{UserName}'", null,
                    commandType: CommandType.Text));
            var IDNumberRequest = Task.FromResult(_dapper.Get<UsersModel>($"SELECT DISTINCT IDNumber  FROM [dbo].[tblUsers] WHERE [IDNumber]='{UserName}' OR [Persal]='{UserName}' OR [Passport]='{UserName}'", null,
                    commandType: CommandType.Text));

            var IDNumber = IDNumberRequest.Result.IDNumber;
            var IDNumberConverted = IDNumber;

            string parentMobileNumber = parentMobileRequest.Result.Cell;

            var dbparams = new DynamicParameters();
            dbparams.Add("@OtpNumber", randomOTP);
            dbparams.Add("@MobileNumber", parentMobileNumber);
            dbparams.Add("@OtpTimeExpiry", DateTime.Now.AddHours(4));
            dbparams.Add("@Message", string.Format("Never share this OTP with anyone:{0} Queries? 0861004000. Sent {1}", randomOTP, DateTime.Now.ToString()));
            dbparams.Add("@IsActive", true);
            dbparams.Add("@IsOfficial", 1);
            dbparams.Add("@Identity", IDNumber);
            dbparams.Add("@Id", ParameterDirection.Output);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_SendOTP]"
               , dbparams,
               commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id");

            SendSMS(parentMobileNumber, string.Format("Never share this OTP with anyone:{0} Queries? 0861004000. Sent {1}", randomOTP, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));

            return randomOTP;
        }
        [HttpPost(nameof(AuthenticateOTP))]
        public async Task<bool> AuthenticateOTP(string MobileNumber, string OTP)
        {
            var currentDate = DateTime.Now;
            var result = await Task.FromResult(_dapper.Get<SMSMessageModel>($"Select [OtpTransactionId] Id,[OtpNumber],[MobileNumber],[OtpTimeCreated],[OtpTimeExpiry],[IsActive],[Message] from [OTPTransaction] WHERE [MobileNumber] = {MobileNumber} AND [OtpNumber]= {OTP} AND [OtpTimeExpiry]>'{currentDate}' AND [IsActive=1]", null,
                    commandType: CommandType.Text));

            if (result != null)
            {
                //Update the OTP status to prevent reuse
                await Task.FromResult(_dapper.Get<SMSMessageModel>($"UPDATE [OTPTransaction] SET IsActive=0 Where OtpTransactionId = {result.Id}", null,
                     commandType: CommandType.Text));
                return true;
            }

            return false;
        }

        [HttpGet(nameof(ResendOTP))]
        public bool ResendOTP(int Id)
        {
            var smsmessage = Task.FromResult(_dapper.Get<SMSMessageModel>($"SELECT * FROM [OTPTransaction] WHERE Id = {Id} AND IsActive=1", null,
                    commandType: CommandType.Text));

            //TODO: send sms again
            return true;

        }

        //Send contact by sms
        [HttpPost(nameof(SendMessage))]
        public async Task<bool> SendMessage(string MobileNumber, string Message)
        {
            SendSMS(MobileNumber, Message);
            return true;
        }

        private async void SendSMS(string MobileNumber, string Message)
        {
            var client = new HttpClient();
            await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://www.xml2sms.gsm.co.za/send?username=anisadefreitas&password=education16&number=" + MobileNumber + "&message=" + Message));

        }

        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            string sOTP = string.Empty;
            string sTempChars = string.Empty;
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
        //Mnqobi welcome sms



        [HttpPost(nameof(SendWelcomeSMS))]

        [ProducesResponseType(StatusCodes.Status200OK)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        public string SendWelcomeSMS(string Id, string Password)

        {

            var result = Task.FromResult(_dapper.Get<UsersModel>($"Select * from [tblUsers] where UserId = {Id}", null, commandType: CommandType.Text));


            string message = $"Dear {result.Result.FirstName + " " + result.Result.Surname}, You are currently registered on the system. Your username is {result.Result.Username} and your temporary password is {Password}.";

            SendSMS(result.Result.Cell, message);

            return message;

        }

        //Mnqobi send user reset otp

        //[HttpPost(nameof(SendWelcomeSMS))]

        //[ProducesResponseType(StatusCodes.Status200OK)]

        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        //public string SendWelcomeSMS(string usernumber, string firstName, string password, string Cell)

        //{

        //    // var result =  Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [tblUsers] WHERE [Persal] = '{Id}' OR [IDNumber]='{Id}' OR [Password]='{Id}'", null, commandType: CommandType.Text));



        //    string message = $"Dear {firstName}, You are currently registered on the system. Your username is {usernumber} and your temporary password is {password}.";

        //    SendSMS(Cell, message);

        //    return message;

        //}
    }
}