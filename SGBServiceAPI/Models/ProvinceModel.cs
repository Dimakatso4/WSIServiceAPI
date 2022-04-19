using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class ProvinceModel
    {
        public int DistrictId { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceCode { get; set; }
        public List<DistrictModel> Districts { get; set; }

    }
}
