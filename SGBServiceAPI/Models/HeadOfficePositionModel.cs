using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class HeadOfficePositionModel
    {
        public string BRANCH { get; set; }
        public string CHIEFDIRECTORATE { get; set; }
        public string DIRECTORATE { get; set; }
        public string SUBDIRECTORATES { get; set; }
        public string POSITION { get; set; }
        public int ID { get; set; }
    }
}
