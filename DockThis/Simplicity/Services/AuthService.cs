using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simplicity.Services.Interfaces;

namespace Simplicity.Services
{
    public class AuthService : IAuthService
    {
        public async Task<bool> Login(string username, string password)
        {
            return true;
        }

        public async Task<bool> UsernameIsTaken(string username)
        {
            return false;
        }

        public async Task CreateAccount(string username, string password)
        {
            return;
        }
    }
}
