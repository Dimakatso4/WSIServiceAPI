using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class PasswordResetModel
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
