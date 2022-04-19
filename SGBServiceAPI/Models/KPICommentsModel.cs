using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class KPICommentsModel
    {
        public int CommentID { get; set; }
        public int SchoolKPIID { get; set; }
        public string Comment { get; set; }
        public DateTime DateCaptured { get; set; }
        public int UserID { get; set; }
    }
}
