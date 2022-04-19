using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SSEComponentModel
    {
        public int ManageComponentID { get; set; }
        public int ManageAreaOfEvaluationID { get; set; }
        public string ComponentName { get; set; }
        public string FocusArea { get; set; }
        public List<ManageKPIModel> ManageKPIs { get; set; }
    }
}
