using System;

namespace WSIServiceAPI.Models
{
    public class ManagementPlanCalendarModel
    {
        public int PlanID { get; set; }
        public string ActivityName { get; set; }
        public string SubActivity { get; set; }
        public DateTime MainStartDate { get; set; }
        public DateTime MainEndDate { get; set; }
        public DateTime SubStartDate { get; set; }
        public DateTime SubEndDate { get; set; }
    }

}
