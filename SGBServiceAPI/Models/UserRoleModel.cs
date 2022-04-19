namespace WSIServiceAPI.Models
{
    public class UserRoleModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        //public string EmisCode { get; set; }
        //public string DistrictCode { get; set; }
        public string rolename { get; set; }
        public string Position { get; set; }
        public string Officelevel { get; set; }
    }
}
