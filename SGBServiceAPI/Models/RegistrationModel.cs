using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class RegistrationModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Persal { get; set; }
        public string IDNumber { get; set; }
        public string Passport { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public string OfficeLevel { get; set; }
        public string SchoolName { get; set; }
        public string DistrictName { get; set; }
        public string Directorate { get; set; }
        public string SubDirectorate { get; set; }
        public string Branch { get; set; }
        public string ChiefDirectorate { get; set; }
        public string Position { get; set; }
        public string Region { get; set; }
        public string UserType { get; set; }
        public string RoleType { get; set; }
        public string DistrictCode { get; set; }
        public string EmisNumber { get; set; }
        public DateTime? DateCaptured { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public int ActivatedBy { get; set; }
        public DateTime? DateActivated { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string ReportingManager { get; set; }
        public int UserId { get; set; }

    }
}
