using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SchoolMainModel
    {
        public string Region { get; set; }
        public string DistrictName { get; set; }
        public string DistrictCode { get; set; }
        public string CircuitNo { get; set; }
        public string ClusterNo { get; set; }
        public string InstitutionName { get; set; }
        public float EmisNumber { get; set; }
        public string InstLevelBudgetaryRequirements { get; set; }
        public string Level { get; set; }
        public string TypeofInstitution { get; set; }
        public string Sector { get; set; }
        public string NoFee { get; set; }
        public int SchoolId { get; set; }
    }
}