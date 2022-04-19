using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class NewsFeedModel
    {
        public int newsFeedID { get; set; }
        public DateTime dateGenerated { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public String title { get; set; }
        public String author { get; set; }
        public String message { get; set; }
        public int newsFeedType { get; set; }
        public String newsFeedImages { get; set; }
        public string description { get; set; }

    }
}
