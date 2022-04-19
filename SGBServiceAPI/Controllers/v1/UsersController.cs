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
    public class UsersController : ControllerBase
    {
        private readonly IDapper _dapper;
        //private readonly IHashingService _hashingService;

        public UsersController(IDapper dapper)
        {
            _dapper = dapper;
            //_hashingService = hashingService;
        }

        [HttpPost(nameof(CreateUser))]
        public async Task<int> CreateUser(UsersModel data)
        {
            //string tempPassword = LoginController.GenerateRandomAlphanumericString();
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@UserId", data.UserId, DbType.Int32, ParameterDirection.Output);
            dataBaseParams.Add("@Citizenship", data.Citizenship);
            dataBaseParams.Add("@Persal", data.Persal);
            dataBaseParams.Add("@IDNumber", data.IDNumber);
            dataBaseParams.Add("@FirstName", data.FirstName);
            dataBaseParams.Add("@Surname", data.Surname);
            dataBaseParams.Add("@Gender", data.Gender);
            dataBaseParams.Add("@HouseNumber", data.HouseNumber);
            dataBaseParams.Add("@ComplexName", data.ComplexName);
            dataBaseParams.Add("@StreetName", data.StreetName);
            dataBaseParams.Add("@Suburb", data.Suburb);
            dataBaseParams.Add("@Section", data.Section);
            dataBaseParams.Add("@City", data.City);
            dataBaseParams.Add("@Cell", data.Cell);
            dataBaseParams.Add("@Email", data.Email);
            dataBaseParams.Add("@Password", data.Password);
            dataBaseParams.Add("@UserActive", data.UserActive);
            dataBaseParams.Add("@UserType", data.UserType);
            dataBaseParams.Add("@OfficeLevel", data.OfficeLevel);
            dataBaseParams.Add("@SchoolName", data.SchoolName);
            dataBaseParams.Add("@DistrictName", data.DistrictName);
            dataBaseParams.Add("@Circuit", data.Circuit);
            dataBaseParams.Add("@Cluster", data.Cluster);
            dataBaseParams.Add("@Directorate", data.Directorate);
            dataBaseParams.Add("@SubDirectorate", data.SubDirectorate);
            dataBaseParams.Add("@Position", data.Position);
            dataBaseParams.Add("@BusinessUnit", data.BusinessUnit);
            dataBaseParams.Add("@Branch", data.Branch);
            dataBaseParams.Add("@ChiefDirectorate", data.ChiefDirectorate);
            dataBaseParams.Add("@Passport", data.Passport);
            dataBaseParams.Add("@Region", data.Region);
            dataBaseParams.Add("@Roletype", data.Roletype);
            dataBaseParams.Add("@Username", data.Username);
            dataBaseParams.Add("@Comment", data.Comment);
            dataBaseParams.Add("@DistrictCode", data.DistrictCode);
            dataBaseParams.Add("@EmisNumber", data.EmisNumber);


            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddUser]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("UserId");
            return returnVal;
        }
        [HttpGet(nameof(GetUserList))]
        public Task<List<UsersModel>> GetUserList()
        {
            var Output = Task.FromResult(_dapper.GetAll<UsersModel>($"select * from [dbo].[tblUsers]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        /*[HttpDelete(nameof(DeleteRoles))]
        public async Task<int> DeleteRoles(int RoleId)
        {
            var Output = await Task.FromResult(_dapper.Execute($"DELETE FROM [dbo].[tblRoles] WHERE [RoleId] = {RoleId}", null, commandType: CommandType.Text));
            return Output;
        }*/
        [HttpPost(nameof(UpdateUser))]
        public Task<int> UpdateUser(UsersModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@UserId", data.UserId, DbType.Int32);
            dataBaseParams.Add("@Citizenship", data.Citizenship);
            dataBaseParams.Add("@Persal", data.Persal);
            dataBaseParams.Add("@IDNumber", data.IDNumber);
            dataBaseParams.Add("@FirstName", data.FirstName);
            dataBaseParams.Add("@Surname", data.Surname);
            dataBaseParams.Add("@Gender", data.Gender);
            dataBaseParams.Add("@HouseNumber", data.HouseNumber);
            dataBaseParams.Add("@ComplexName", data.ComplexName);
            dataBaseParams.Add("@StreetName", data.StreetName);
            dataBaseParams.Add("@Suburb", data.Suburb);
            dataBaseParams.Add("@Section", data.Section);
            dataBaseParams.Add("@City", data.City);
            dataBaseParams.Add("@Cell", data.Cell);
            dataBaseParams.Add("@Email", data.Email);
            dataBaseParams.Add("@Password", data.Password);
            dataBaseParams.Add("@UserActive", data.UserActive);
            dataBaseParams.Add("@UserType", data.UserType);
            dataBaseParams.Add("@OfficeLevel", data.OfficeLevel);
            dataBaseParams.Add("@SchoolName", data.SchoolName);
            dataBaseParams.Add("@DistrictName", data.DistrictName);
            dataBaseParams.Add("@Circuit", data.Circuit);
            dataBaseParams.Add("@Cluster", data.Cluster);
            dataBaseParams.Add("@Directorate", data.Directorate);
            dataBaseParams.Add("@SubDirectorate", data.SubDirectorate);
            dataBaseParams.Add("@Position", data.Position);
            dataBaseParams.Add("@BusinessUnit", data.BusinessUnit);
            dataBaseParams.Add("@Branch", data.Branch);
            dataBaseParams.Add("@ChiefDirectorate", data.ChiefDirectorate);
            dataBaseParams.Add("@Passport", data.Passport);
            dataBaseParams.Add("@Region", data.Region);
            dataBaseParams.Add("@Roletype", data.Roletype);
            dataBaseParams.Add("@Username", data.Username);
            dataBaseParams.Add("@DistrictCode", data.DistrictCode);
            dataBaseParams.Add("@EmisNumber", data.EmisNumber);

            var updateU = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateUser]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateU;
        }
        [HttpGet(nameof(GetRole))]
        public Task<List<Rolemodel>> GetRole()
        {
            var Output = Task.FromResult(_dapper.GetAll<Rolemodel>($"Select distinct ID,RoleId,rolename from tblUserRole", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpPost(nameof(UpdateInActiveUser))]
        public Task<int> UpdateInActiveUser(int UserId, string UserActive)

        {
            var dbPara = new DynamicParameters();
            dbPara.Add("@UserId", UserId);
            dbPara.Add("@UserActive", UserActive);



            var UpdateInActiveUser = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateUserActive]",
            dbPara,
            commandType: CommandType.StoredProcedure));
            return UpdateInActiveUser;
        }

        [HttpGet(nameof(IsCellphoneAvailable))]
        public Task<bool> IsCellphoneAvailable(string Cellphonenumber)
        {
            var results = Task.FromResult(_dapper.GetAll<string>($"Select cell from [tblUsers] WHERE cell='{Cellphonenumber}'", null,
                    commandType: CommandType.Text));

            if (results.Result.Count > 0)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);

        }
        [HttpGet(nameof(GetUserById))]

        public Task<UsersModel> GetUserById(string Id)
        {
            var Output = Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [dbo].[tblUsers] WHERE [UserId]='{Id}'", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetUserByPersalOrPassport))]
        public Task<UsersModel> GetUserByPersalOrPassport(string Id)
        {
            var Output = Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [dbo].[tblUsers] WHERE [Persal]='{Id}' OR [Passport]='{Id}' OR [IDNumber]='{Id}'", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(IsPersalAvailable))]
        public Task<bool> IsPersalAvailable(string PersalNumber)
        {
            var results = Task.FromResult(_dapper.GetAll<string>($"Select Persal from [tblUsers] WHERE Persal='{PersalNumber}'", null,
                    commandType: CommandType.Text));

            if (results.Result.Count > 0)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);

        }
        [HttpGet(nameof(GetOfficeLevel))]
        public Task<List<NewOfficeLevelModel>> GetOfficeLevel()
        {
            var Output = Task.FromResult(_dapper.GetAll<NewOfficeLevelModel>($"select * from[dbo].[tblNewOfficeLevel]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetGender))]
        public Task<List<NewGenderModel>> GetGender()
        {
            var Output = Task.FromResult(_dapper.GetAll<NewGenderModel>($"select * from[dbo].[tblNewGender]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetCitizenship))]
        public Task<List<NewCitizenshipModel>> GetCitizenship()
        {
            var Output = Task.FromResult(_dapper.GetAll<NewCitizenshipModel>($"select * from[dbo].[tblNewCitizenship]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetSchoolPosition))]
        public Task<List<NewSchoolPositionModel>> GetSchoolPosition()
        {
            var Output = Task.FromResult(_dapper.GetAll<NewSchoolPositionModel>($"select * from[dbo].[tblNewSchoolPosition]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetRoleNamebyUserId))]
        public Task<List<RoleNamebyUsersIdModel>> GetRoleNamebyUserId(int Id)
        {
            var Output = Task.FromResult(_dapper.GetAll<RoleNamebyUsersIdModel>($"select UserId,tblUsers.OfficeLevel,tblUserRole.rolename,tblUserRole.RoleId from tblUsers join tblUserRole on tblUserRole.Position =tblUsers.Position where UserId={Id}", null,
                    commandType: CommandType.Text));
            return Output;
        }

        //Get employee by persal or ID number
        [HttpGet(nameof(GetEmployeeByPersalOrIDNumber))]
        public async Task<UsersModel> GetEmployeeByPersalOrIDNumber(string Id)
        {
            if (Id.Length < 13)
            {

                var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from [tblUsers] where Persal = '{Id}'", null, commandType: CommandType.Text));
                return result;
            }
            else
            {
                var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from [tblUsers] where IDNumber = '{Id}'", null, commandType: CommandType.Text));
                return result;
            }
        }

        //Get non gde employee by ID
        [HttpGet(nameof(GetUserByIDNumberOrPassport))]
        public async Task<UsersModel> GetUserByIDNumberOrPassport(string Id)
        {
            //if (Id.Length == 13)
            //{
                var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from [tblUsers] where IDNumber = '{Id}'", null, commandType: CommandType.Text));
                return result;
           // }
           // else
            //{
                //var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from [tblUsers] where Passport = '{Id}'", null, commandType: CommandType.Text));
                //return result;

            //}
        }

        //Mnqobi generate temporary password for a user
        [HttpPost(nameof(ResetUserPassword))]
        public async Task<string> ResetUserPassword(int Id)
        {
            var tempPass = LoginUserController.GenerateRandomAlphanumericString();
            var dbPara = new DynamicParameters();
            dbPara.Add("@Id", Id);
            //dbPara.Add("Credentials", LoginController.GenerateTempPassword());
            dbPara.Add("@Password", tempPass);
       

            var updateUser = await Task.FromResult(_dapper.Update<string>("[dbo].[SP_ResetPassword]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return tempPass;

        }


        //Mnqobi confirm password reset
        [HttpGet(nameof(ValidatePassword))]
        public async Task<bool> ValidatePassword(int Id, string Pass)
        {
            var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from [tblUsers] where UserId = {Id} and Password = '{Pass}' ", null, commandType: CommandType.Text));

            if (result != null)
                return true;
            else
                return false;
        }

        [HttpPost(nameof(UpdateUserReset))]
        public Task<int> UpdateUserReset(int Id, string Credentials)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", Id);
            //string hash = _hashingService.HashPassword(Credentials);
            dbPara.Add("Credentials", Credentials);

            var result = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateUser]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return result;
        }



        [HttpGet(nameof(GetuserByPersaNumber))]
        public async Task<UsersModel> GetuserByPersaNumber(string Persal)
        {

           
                  // SELECT * FROM tblUsers WHERE Persal = '29088186'


            //if (Id.Length == 13)
            //{
            var result = await Task.FromResult(_dapper.Get<UsersModel>($" SELECT * FROM tblUsers WHERE Persal = '{Persal}'", null, commandType: CommandType.Text));
            return result;
            
            // }
            // else
            //{
            //var result = await Task.FromResult(_dapper.Get<UsersModel>($"Select * from [tblUsers] where Passport = '{Id}'", null, commandType: CommandType.Text));
            //return result;

            //}
        }


        [HttpPost(nameof(CaptureNewPassword))]
        public async Task<TUsersModel> CaptureNewPassword(int Id, string Password)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("@Id", Id);
            dbPara.Add("@Password", Password);
            //string hash = _hashingService.HashPassword(Credentials);

            var reset = Task.FromResult(_dapper.Update<int>("[dbo].[SP_ResetPassword]", dbPara, commandType: CommandType.StoredProcedure));

            var user = await Task.FromResult(_dapper.Get<TUsersModel>($"SELECT * from [tblUsers] where UserId = {Id}", null, commandType: CommandType.Text));


            return user;
        }

        [HttpGet(nameof(GetUserRole))]
        public Task<List<UserRoleModel>> GetUserRole()
        {
            var Output = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from tblUserRole order by Position", null,
                    commandType: CommandType.Text));
            return Output;
        }


        [HttpGet(nameof(GetUserRole2))]
        public Task<List<UserRoleModel>> GetUserRole2()
        {
            var Output = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select ur.*, u.FirstName + ' ' + u.[Surname] + ' (' + ur.Position + ')' as Position from [dbo].[tblUserRole] ur Inner JOIN[dbo].[tblUsers] u on u.Position = ur.Position", null,
                    commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetBranch))]
        public Task<List<BranchModel>> GetBranch()
        {
            var Output = Task.FromResult(_dapper.GetAll<BranchModel>($"select * from [dbo].[tblBranch]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetChiefDirectorate))]
        public Task<List<ChiefDirectorateModel>> GetChiefDirectorate()
        {
            var Output = Task.FromResult(_dapper.GetAll<ChiefDirectorateModel>($"select * from [dbo].[tblChiefDirectorate]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetDirectorate))]
        public Task<List<DirectorateModel>> GetDirectorate()
        {
            var Output = Task.FromResult(_dapper.GetAll<DirectorateModel>($"select * from [dbo].[tblDirectorate]", null,
                    commandType: CommandType.Text));
            return Output;
        }
        [HttpGet(nameof(GetSubDirectorate))]
        public Task<List<SubDirectorateModel>> GetSubDirectorate()
        {
            var Output = Task.FromResult(_dapper.GetAll<SubDirectorateModel>($"select * from [dbo].[tblSubDirectorate]", null,
                    commandType: CommandType.Text));
            return Output;
        }


        [HttpPost(nameof(CreateUpdateApplication))]
        public async Task<int> CreateUpdateApplication(RegistrationModel data)
        {
            //string tempPassword = LoginController.GenerateRandomAlphanumericString();
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@Id", data.Id, DbType.Int32);
            dataBaseParams.Add("@Firstname", data.Firstname);
            dataBaseParams.Add("@Surname", data.Surname);
            dataBaseParams.Add("@Persal", data.Persal);
            dataBaseParams.Add("@IDNumber", data.IDNumber);
            dataBaseParams.Add("@Passport", data.Passport);
            dataBaseParams.Add("@Nationality", data.Nationality);
            dataBaseParams.Add("@Gender", data.Gender);
            dataBaseParams.Add("@Cell", data.Cell);
            dataBaseParams.Add("@Email", data.Email);
            dataBaseParams.Add("@OfficeLevel", data.OfficeLevel);
            dataBaseParams.Add("@SchoolName", data.SchoolName);
            dataBaseParams.Add("@DistrictName", data.DistrictName);
            dataBaseParams.Add("@Directorate", data.Directorate);
            dataBaseParams.Add("@SubDirectorate", data.SubDirectorate);
            dataBaseParams.Add("@Branch", data.Branch);
            dataBaseParams.Add("@ChiefDirectorate", data.ChiefDirectorate);
            dataBaseParams.Add("@Position", data.Position);
            dataBaseParams.Add("@Region", data.Region);
            dataBaseParams.Add("@UserType", data.UserType);
            dataBaseParams.Add("@RoleType", data.RoleType);
            dataBaseParams.Add("@DistrictCode", data.DistrictCode);
            dataBaseParams.Add("@EmisNumber", data.EmisNumber);
            dataBaseParams.Add("@DateCaptured", data.DateCaptured);
            dataBaseParams.Add("@ApprovedBy", data.ApprovedBy);
            dataBaseParams.Add("@DateApproved", data.DateApproved);
            dataBaseParams.Add("@ActivatedBy", data.ActivatedBy);
            dataBaseParams.Add("@DateActivated", data.DateActivated);
            dataBaseParams.Add("@Comment", data.Comment);
            dataBaseParams.Add("@Status", data.Status);
            dataBaseParams.Add("@ReportingManager", data.ReportingManager);
            dataBaseParams.Add("@UserId", data.UserId);


            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertorUpdateRegistration]",
                dataBaseParams, commandType: CommandType.StoredProcedure));

            var returnVal = dataBaseParams.Get<int>("@Id");
            return returnVal;
        }


        [HttpGet(nameof(GetAllRegistration))]
        public Task<List<RegistrationModel>> GetAllRegistration()
        {
            var Output = Task.FromResult(_dapper.GetAll<RegistrationModel>($"SELECT * FROM [dbo].[tblRegistration]", null,
                    commandType: CommandType.Text));
            return Output;
        }

        //Get non gde employee by ID
        [HttpGet(nameof(GetRegistrationById))]
        public async Task<RegistrationModel> GetRegistrationById(string Id)
        {

            var result = await Task.FromResult(_dapper.Get<RegistrationModel>($"SELECT * FROM [dbo].[tblRegistration] WHERE [Id] = {Id}", null, commandType: CommandType.Text));
            return result;
        }


        [HttpGet(nameof(GetRegistrationByStatus))]
        public Task<List<RegistrationModel>> GetRegistrationByStatus(string Status)
        {
            var Output = Task.FromResult(_dapper.GetAll<RegistrationModel>($"SELECT * FROM [dbo].[tblRegistration] WHERE [Status] = '{Status}'", null,
                    commandType: CommandType.Text));
            return Output;
        }


        //Get non gde employee by ID
        [HttpGet(nameof(GetRegistrationByIdentification))]
        public async Task<RegistrationModel> GetRegistrationByIdentification(string Number)
        {

            var result = await Task.FromResult(_dapper.Get<RegistrationModel>($"SELECT * FROM [dbo].[tblRegistration] WHERE [Persal] = '{Number}' OR [IDNumber] = '{Number}' OR [Passport] = '{Number}' ", null, commandType: CommandType.Text));
            return result;
        }


        [HttpPost(nameof(UpdateUserId))]
        public Task<int> UpdateUserId(int Id, int UserId)
        {

            var result = Task.FromResult(_dapper.Update<int>($"UPDATE [dbo].[tblRegistration] SET [UserId] = {UserId} WHERE [Id] = {Id}",
                null, commandType: CommandType.Text));
            return result;
        }


    }
}