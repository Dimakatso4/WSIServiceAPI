using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class DistrictManagementPlanModel
    {
        public int Id { get; set; }
        public string SubActivity { get; set; }
        public string Responsibility { get; set; }

        public List<UserRoleModel> ResponsibilityList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ManagementPlanActivityId { get; set; }
        public string DistrictCode { get; set; }
        public int StatusID { get; set; }
        public string Stutus { get; set; }
        public int PeriodID { get; set; }
        public string Period { get; set; }
        public string Branch { get; set; }
        public string Directorate { get; set; }
        public string SubDirectorate { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string ChiefDirectorate { get; set; }
        public string OfficeLevel { get; set; }
        public string ResponsibilityType { get; set; }
    }
}
