using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class ManageKPIModel
    {
        public int ManageKPIID { get; set; }
        public string KPIName { get; set; }
        public int ManageAreaOfEvaluationID { get; set; }
        public int ManageComponentID { get; set; }
        public int CompulsoryID { get; set; }
        public int RatingId { get; set; }
        public int BusinessUnitID { get; set; }
        public int LegislationID { get; set; }
        public string Resources { get; set; }
        public string FocusArea { get; set; }
        public string ComponentName { get; set; }
        public string CompulsoryName { get; set; }
        public string RatingName { get; set; }
        public string BusinessName { get; set; }
        public string LegislationName { get; set; }
    }
}
