using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SchoolModel
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolAddress { get; set; }
        public string District { get; set; }
        public string ClusterNumber { get; set; }
        public int TellNumber { get; set; }
        public string EmailAddress { get; set; }
        public int FaxNumber { get; set; }
        public int CellphoneNumber { get; set; }
        public string SchoolType { get; set; }
        public string Province { get; set; }
        public int Code { get; set; }
        public string Region { get; set; }
    }
}
