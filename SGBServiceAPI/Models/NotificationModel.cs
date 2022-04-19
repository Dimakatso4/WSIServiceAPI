using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
	public class NotificationModel
	{
		public int NotificationID {get; set;}
		public int UserId { get; set; }
		public string Firstname { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string OtpNumber { get; set; }
		public int PhoneNumber { get; set; }
		public DateTime OtpTimeCreated { get; set; }
		public DateTime OtpTimeExpiry { get; set; }
		public bool IsActive { get; set; }
		public string Message { get; set; }
	}
}
