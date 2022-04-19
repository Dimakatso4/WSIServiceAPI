using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class RoleNamebyUsersIdModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string rolename { get; set; }
        public string OfficeLevel { get; set; }
    }
}
