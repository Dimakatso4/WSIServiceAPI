using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class SubDirectorateModel
    {
        public int Id { get; set; }
        public string DirectorateId { get; set; }
        public string SubDirectorateName { get; set; }
    }
}
