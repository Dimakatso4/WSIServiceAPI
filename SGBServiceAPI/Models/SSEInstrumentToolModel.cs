using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SSEInstrumentToolModel
    {
        public int Id { get; set; }
        public string SSEInstrumentName { get; set; }
        public string Year { get; set; }
        public string KPIArrayId { get; set; }
        public string KPIJson { get; set; }
        public string SSEAuditTrail { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int StatusID { get; set; }
        public int ManageAreaOfEvaluationID { get; set; }
        public int ManageComponentID { get; set; }


    }
}
