using System;

namespace WSIServiceAPI.Models
{
    public class SIPActionPlanModel
    {
        public int SIPActionId { get; set; }
        public string AreaOfDevelopment { get; set; }
        public string DescriptionOfActivities { get; set; }
        public string TargetGroup { get; set; }
        public string Responsibility { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Resources { get; set; }
        public string CompletedJSON { get; set; }
        public string ProgressPerQuarter { get; set; }
        public string ProgressPerQuarter1 { get; set; }
        public string ProgressPerQuarter2 { get; set; }
        public string ProgressPerQuarter3 { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string DistrictCode { get; set; }
        public int SchoolId { get; set; }
        public bool IsCompleted { get; set; }
        public int KPIID { get; set; }
        public int EmisNumber { get; set; }
        public decimal CalculateValue { get; set; }
        public int Quarter { get; set; }
        public int UserId { get; set; }
        public bool IsApproved { get; set; }
        public decimal Score { get; set; }
        public string SIPClusterComments { get; set; }
        public string DIPBUComment { get; set; }
        public string DIPCircuitComent { get; set; }
        public string DIPDirectComment { get; set; }
        public string PIPBuComment { get; set; }
        public string PIPLineDirectorComment { get; set; }
        public string PIPChiefComment { get; set; }
        public string PIPDepComment { get; set; }
    }
}
