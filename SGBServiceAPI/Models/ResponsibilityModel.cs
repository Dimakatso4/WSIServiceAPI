using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class ResponsibilityModel
    {
        public int ID { get; set; }
        public string Responsibility { get; set; }
        public int RoleID { get; set; }
    }
}
