using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class DistrictModel
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string DistrictCode { get; set; }
        public string Province { get; set; }
        public List<SchoolNamesModel> Schools { get; set; }

    }
}
