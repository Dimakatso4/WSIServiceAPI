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
    public class PasswordResetController : ControllerBase
    {
        public Task<int> Output;
        public int update;
        public int updateOTPstatus;
        private readonly IDapper _dapper;
        public PasswordResetController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPatch(nameof(ResetPassword))]
        public Task<int> ResetPassword(PasswordResetModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@UserId", data.UserId, DbType.Int32);
            dataBaseParams.Add("@Password", data.Password);

            var updateU = Task.FromResult(_dapper.Update<int>("[dbo].[spResetpassword]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateU;
        }
        [HttpPatch(nameof(AuthethicateOTP))]
        public async Task<int> AuthethicateOTP(string usernumber, string userOTP, string newPassword)
        {



            //getOTPfrom the database

            var generatedUserOTP = Task.FromResult(_dapper.Get<SMSMessageModel>($"SELECT DISTINCT [OtpNumber] FROM [dbo].[OTPTransaction] WHERE [Identity]='{usernumber}' AND [OtpNumber]='{userOTP}'  AND [IsActive]=1", null,
                commandType: CommandType.Text));

            //var generatedOTP = generatedUserOTP.Result.OtpNumber;
            //get identity
            if (generatedUserOTP != null)
            {

                this.update = await Task.FromResult(_dapper.Update<int>($"UPDATE [dbo].[tblUsers] SET [Password]='{newPassword}' WHERE [IDNumber]= '{usernumber}'  OR [Persal]= '{usernumber}' OR [Passport]='{usernumber}'",
                                null, commandType: CommandType.Text));
                this.updateOTPstatus = await Task.FromResult(_dapper.Update<int>($"UPDATE [dbo].[OTPTransaction] SET [IsActive]=0 WHERE [Identity]= '{usernumber}'",
                                null, commandType: CommandType.Text));

            }

            // var Output = Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [dbo].[tblUsers] WHERE [IDNumber]= '{usernumber}' OR [Persal]='{usernumber}'", null, commandType: CommandType.Text));

            // var dataBaseParams = new DynamicParameters();

            // UsersModel user = Output.Result;






            return this.update;


        }
        [HttpPatch(nameof(AuthethicateTempPassword))]
        public async Task<int> AuthethicateTempPassword(string usernumber, string temp, string newPassword)
        {



            //getOTPfrom the database

            var getUserTemp = Task.FromResult(_dapper.Get<SMSMessageModel>($"SELECT DISTINCT [Temp] FROM [dbo].[OTPTransaction] WHERE [Identity]='{usernumber}' AND [Temp]='{temp}'  AND [IsActive]=1", null,
                commandType: CommandType.Text));

            //var generatedOTP = generatedUserOTP.Result.OtpNumber;
            //get identity
            if (getUserTemp != null)
            {

                this.update = await Task.FromResult(_dapper.Update<int>($"UPDATE [dbo].[tblUsers] SET [Password]='{newPassword}' WHERE [IDNumber]= '{usernumber}'  OR [Persal]= '{usernumber}' OR [Passport]='{usernumber}'",
                                null, commandType: CommandType.Text));
                this.updateOTPstatus = await Task.FromResult(_dapper.Update<int>($"UPDATE [dbo].[OTPTransaction] SET [IsActive]=0 WHERE [Identity]= '{usernumber}'",
                                null, commandType: CommandType.Text));

            }

            // var Output = Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [dbo].[tblUsers] WHERE [IDNumber]= '{usernumber}' OR [Persal]='{usernumber}'", null, commandType: CommandType.Text));

            // var dataBaseParams = new DynamicParameters();

            // UsersModel user = Output.Result;






            return this.update;


        }

        //Mnqobi

        [HttpPost(nameof(TemporalPassword))]
        public async Task<int> TemporalPassword(string usernumber, string temp, string newPassword)
        {



            //getOTPfrom the database

            var getUserTemp = Task.FromResult(_dapper.Get<SMSMessageModel>($"SELECT DISTINCT [Temp] FROM [dbo].[OTPTransaction] WHERE [Identity]='{usernumber}' AND [Temp]='{temp}'  AND [IsActive]=1", null,
                commandType: CommandType.Text));

            //var generatedOTP = generatedUserOTP.Result.OtpNumber;
            //get identity
            if (getUserTemp != null)
            {

                this.update = await Task.FromResult(_dapper.Update<int>($"UPDATE [dbo].[tblUsers] SET [Password]='{newPassword}' WHERE [IDNumber]= '{usernumber}'  OR [Persal]= '{usernumber}' OR [Passport]='{usernumber}'",
                                null, commandType: CommandType.Text));
                this.updateOTPstatus = await Task.FromResult(_dapper.Update<int>($"UPDATE [dbo].[OTPTransaction] SET [IsActive]=0 WHERE [Identity]= '{usernumber}'",
                                null, commandType: CommandType.Text));

            }

            // var Output = Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [dbo].[tblUsers] WHERE [IDNumber]= '{usernumber}' OR [Persal]='{usernumber}'", null, commandType: CommandType.Text));

            // var dataBaseParams = new DynamicParameters();

            // UsersModel user = Output.Result;






            return this.update;


        }

    }
}
