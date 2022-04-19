using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class RegionModel
    {
        public int RegionId { get; set; }
        public int OfficeLevelId { get; set; }
        public string Region { get; set; }
        public string DistrictName { get; set; }
    }
}
