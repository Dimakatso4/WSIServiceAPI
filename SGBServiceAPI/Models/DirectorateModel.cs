using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class DirectorateModel
    {
        public int Id { get; set; }
        public string ChiefDirectorateId { get; set; }
        public string DirectorateName { get; set; }
    }
}
