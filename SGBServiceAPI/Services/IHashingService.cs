using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Services
{
    public interface IHashingService
    {
        string HashPassword(string plainPassword);
        bool CompareHashPassword(string plainPassword, string hash);
    }
}
