using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class CompleteSSEModel
    {
        public int CompleteSSEID { get; set; }
        public int SseQuestionID { get; set; }
        public string Rate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string EvidenceDescription { get; set; }
        public string PrincipalComment { get; set; }
        public string SGBComment { get; set; }
        public string BUAssigned { get; set; }
        public DateTime BUCompleteDate { get; set; }
        public string BUComment { get; set; }
        public string BUSuggestion { get; set; }
        public string BUstatus { get; set; }
        public string Status { get; set; }
        public int SchoolId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int InstrumentId { get; set; }
        public string AreaofEvaluation { get; set; }
        public string InstrumentName { get; set; }
        public string Component { get; set; }
    }
}
