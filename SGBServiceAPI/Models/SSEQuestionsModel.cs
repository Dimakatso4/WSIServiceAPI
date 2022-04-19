using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SSEQuestionsModel
    {
        public int SSEQuestionID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int InstrumentId { get; set; }
    }
}
