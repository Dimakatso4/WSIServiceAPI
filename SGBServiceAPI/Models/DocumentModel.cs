using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class DocumentModel
    {
        public int DocumentId { get; set; }
        public int DocumentNumber { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public int  AreaOfEvaluationID { get; set; }
        public int  DocumentSavedBy { get; set; }
        public int UserID { get; set; }
        public string Status { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DateLastAmended { get; set; }
        public DateTime? DateNextReview { get; set; }
        public string RelatedPolicies { get; set; }
        public string documentPath { get; set; }
        public bool IsActive { get; set; }
        public bool IsCurrent { get; set; }
        public string FocusArea { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}
