using System;

namespace WSIServiceAPI.Models
{
    public class ManagementDocumentModel
    {
        public int DocumentID { set; get; }
        public string DocumentName { set; get; }
        public string DocumentPath { set; get; }
        public DateTime DocumentDateUploaded { set; get; }
        public int UploadedBy { set; get; }
        public DateTime YearUploaded { set; get; }
    }
}
