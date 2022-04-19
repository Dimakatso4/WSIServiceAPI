using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class AreaOfEvaluationModel
    {
        public int ManageAreaOfEvalutionID { get; set; }
        public string FocusArea { get; set; }
        public int Weight { get; set; }
        public int TotalNumber { get; set; }
        public List<SSEComponentModel> SSEComponents { get; set; }
    }
}
