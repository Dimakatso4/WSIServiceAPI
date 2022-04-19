namespace WSIServiceAPI.Models
{
    public class CreateSSEModel
    {
        public bool IsPublishing { get; set; }
        public string CompletedJSON { get; set; }
        public string Status { get; set; }
        public int EmisNumber { get; set; }
        public int UserId { get; set; }
    }
}
