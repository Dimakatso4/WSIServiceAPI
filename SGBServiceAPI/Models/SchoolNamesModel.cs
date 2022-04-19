using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SchoolNamesModel
    {
        public string InstitutionName { get; set; }
        public string EmisNumber { get; set; }
        public string DistrictName { get; set; }
        public string DistrictCode { get; set; }
        public string CircuitNo { get; set; }
        public string ClusterNo { get; set; }
        public string SchoolStatus { get; set; }
        public string InstLevelBudgetaryRequirements { get; set; }
    }
}
