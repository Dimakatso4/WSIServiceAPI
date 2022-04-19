using System;
using System.Collections.Generic;

namespace WSIServiceAPI.Models
{
    public class ManagementPlanModel
    {
        public int PlanID { get; set; }
        public string ActivityName { get; set; }
        public List<UserRoleModel> Responsibility { get; set; }
        public string ResponsibilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusID { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public int PeriodID { get; set; }
        public string ManagementPeriod { get; set; }

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
