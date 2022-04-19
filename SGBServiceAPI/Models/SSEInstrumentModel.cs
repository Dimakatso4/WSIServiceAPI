using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SSEInstrumentModel
    {

        public int SSEInstrumentId { get; set; }
        public int AreaOfEvaluationID { get; set; }
        public int CreatedBy { get; set; }
        public string InstrumentName { get; set; }
        public int SSEStatusID { get; set; }
        public string AreaOfEvaluationName { get; set; }
        public DateTime Year { get; set; }
        public int BusinessUnit { get; set; }
        public int kpiID { get; set; }
        public string status { get; set; }

    }
}
