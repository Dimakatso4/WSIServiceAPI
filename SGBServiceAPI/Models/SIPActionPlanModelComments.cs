using System;

namespace WSIServiceAPI.Models
{
    public class SIPActionPlanModelComments
    {
        public int SipActionPlanCommentsID { get; set; }

        public string Comments { get; set; }

        public string Path { get; set; }
        public string Status { get; set; }
        public int SipActionPlanID { get; set; }
        public string DIPBUComment { get; set; }
        public string? DIPCircuitComent { get; set; }
        public string? DIPDirectComment { get; set; }

    }
}
