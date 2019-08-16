using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(string username, string password);
        Task<bool> UsernameIsTaken(string username);
        Task CreateAccount(string username, string password);
    }
}
