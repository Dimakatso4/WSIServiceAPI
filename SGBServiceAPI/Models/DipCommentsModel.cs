using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class DipCommentsModel
    {
        public int CommentID { get; set; }
        public int DistrictID { get; set; }
        public string Comment { get; set; }
        public DateTime DateCaptured { get; set; }
        public int UserID { get; set; }
        public int DipKPIID { get; set; }
    }
}
