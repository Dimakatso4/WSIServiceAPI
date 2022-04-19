using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SSEInstrumentToolKPIModel
    {
        public int InstrumentListID { get; set; }
        public int SSEInstrumentID { get; set; }
        public string Year { get; set; }
        public int ManagementPlanID { get; set; }
        public DateTime ManagementStartDate { get; set; }
        public DateTime ManagementEndDate { get; set; }
        public int ManageAreaOfEvaluationID { get; set; }
        public string FocusArea { get; set; }
        public int ManageComponentID { get; set; }
        public string ComponentName { get; set; }
        public string KPIName { get; set; }
        public int CompulsoryID { get; set; }
        public string Compulsory { get; set; }
        public int RatingID { get; set; }
        public string Rating { get; set; }
        public string BusinessUnit { get; set; }
        public string Legislation { get; set; }
        public string Resources { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public int ManageKPIID { get; set; }
        public int EmisNumber { get; set; }
        public int CurrentAreaOfEvaluation { get; set; }
        public int PreviousAreaOfEvaluation { get; set; }         
        public int CurrentComponent { get; set; }
        public int PreviousComponent { get; set; }




    }
}
