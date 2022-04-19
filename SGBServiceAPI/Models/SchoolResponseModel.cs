using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SchoolKPIModel
    {
        public int SchoolKPIID { get; set; }
        public int EmisNo { get; set; }
        public int ManageAreaOfEvaluationID { get; set; }
        public int ManageComponentID { get; set; }
        public string KPIName { get; set; }
        public string FocusArea { get; set; }
        public string ComponentName { get; set; }
        public string EvidenceDescription { get; set; }
        public string Evidence { get; set; }

        public int KPIID { get; set; }
        public string Response { get; set; }
        public int UserID { get; set; }
        public int EmisNumber { get; set; }
        public DateTime DateCaptured { get; set; }
        public bool IsSip { get; set; }
        public string Status { get; set; }
        public string CompletedJSON { get; set; }
        public string IsPublishing { get; set; }
        //public List<KPICommentsModel> Comments { get; set; }
        public string Comment { get; set; }
        public string SchoolComment { get; set; }
        public string DocumentPath { get; set; }
        public int CurrentAreaOfEvaluation { get; set; }
        public int PreviousAreaOfEvaluation { get; set; }
                
        public int CurrentComponent { get; set; }
        public int PreviousComponent { get; set; }

        public int Id { get; set; }
        public int[] IDs { get; set; }

        public bool IsPrioritise { get; set; }
        public string Rating { get; set; }
        public List<SchoolNamesModel> DIPSchools { get; set; }

        public List<DistrictModel> Districts { get; set; }
        public float Score { get; set; }
        public float PreviousRating { get; set; }
        public int TotalNumber { get; set; }
        public int CompletedNumber { get; set; }
        public List<SchoolKPIModel> Totals { get; set; }
        public string SIPClusterComments { get; set; }
        public string ManagerComment { get; set; }
        public string DirectorComment { get; set; }
    }
}
