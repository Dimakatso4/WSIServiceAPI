using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SendEmailModel
    {
        public string[] ReceiversList { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string DocumentName { get; set; }
    }
}
