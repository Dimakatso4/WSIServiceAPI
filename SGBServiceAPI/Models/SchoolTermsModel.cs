using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SchoolTermsModel
    {
        public int TermID { get; set; }
		public DateTime Term1Start { get; set; }
		public DateTime Term1End { get; set; }
		public DateTime Term2Start { get; set; }
		public DateTime Term2End { get; set; }
		public DateTime Term3Start { get; set; }
		public DateTime Term3End { get; set; }
		public DateTime Term4Start { get; set; }
		public DateTime Term4End { get; set; }
	
	}
}
