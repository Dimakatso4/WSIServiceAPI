using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class TUsersModel
    {

        public string Persal { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string IDNumber { get; set; }

        public string Position { get; set; }
        public string UserActive { get; set; }
        public string rolename { get; set; }
        public string RoleId { get; set; } 
        public string UserId { get; set; }
        public string Passport { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string DistrictCode { get; set; }
        public string EmisNumber { get; set; }
        public string Directorate { get; set; }
        public string OfficeLevel { get; set; }


    }





}

public class loginReq
{

    public string Passport { get; set; }
    public string Persal { get; set; }
    public string IDNumber { get; set; }
    public string Password { get; set; }

}