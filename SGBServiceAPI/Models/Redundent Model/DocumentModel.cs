using System;

namespace WSIServiceAPI.Models
{
    public class DocumentModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentPath { get; set; }
        public DateTime UploadedDate { get; set; }
        public int UpdloadedBy { get; set; }
    }
}
