using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class KPIQuestionModel
    {
        public int KPIID { get; set; }
        public string Questions { get; set; }
        public int areaOfEvaluationID { get; set; }
        public int CompID { get; set; }
    }
}
