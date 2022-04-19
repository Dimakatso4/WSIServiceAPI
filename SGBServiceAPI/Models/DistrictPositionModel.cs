using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class DistrictPositionModel
    {
        public string DIRECTORATE { get; set; }
        public string SUBDIRECTORATE { get; set; }
        public string POSITION { get; set; }
        public string BUSINESSUNIT { get; set; }
        public int DisctrictPositionId { get; set; }
    }
}