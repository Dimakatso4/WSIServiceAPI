using System;
using System.ComponentModel.DataAnnotations;

namespace WSIServiceAPI.Models
{
    ///<Summary>
    /// CreateUserModel
    ///</Summary>
    public class UserModel
    {
     
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Persal { get; set; }
        public string EmisNumber { get; set; }
        public string DistrictCode { get; set; }
        public string ProvinceId { get; set; }        
        public string Firstname { get; set; }      
        public string Surname { get; set; }
        public string CellNumber { get; set; }
        public string EmailAddress { get; set; }       
        public string IDNumber { get; set; }
        public int Status { get; set; }
        public bool IsNominated { get; set; }
        public bool IsSeconded { get; set; }
        public int ElectionScore { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }


}
