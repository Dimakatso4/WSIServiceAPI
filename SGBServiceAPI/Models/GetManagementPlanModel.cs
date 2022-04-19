using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class GetManagementPlanModel
    {
        public int PlanID { get; set; }
        public string ActivityName { get; set; }
        public string Responsibility { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }

    }
}
