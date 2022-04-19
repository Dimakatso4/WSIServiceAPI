using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class UserTypeModel
    {
        public int UserTypeId { get; set; }
        public String UserType { get; set; }
        public String SchoolPrinicipal { get; set; }
        public String DistrictUsers { get; set; }
        public String HeadOfficeUsers { get; set; }
    }
}
