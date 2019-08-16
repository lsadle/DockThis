using System;
using System.Threading.Tasks;
using Common.Api.Interfaces;
using Common.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Simplicity.Services.Interfaces;

namespace Simplicity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Login(string username, string password)
        {
            var user = await _userService.GetUser(username);

            if (user == null)
            {
                return false;
            }

            string hashed = CreateHash(password, user.Salt);

            if (hashed == password)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UsernameIsTaken(string username)
        {
            var user = await _userService.GetUser(username);

            return user != null;
        }

        public async Task CreateAccount(string username, string password)
        {
            var salt = Guid.NewGuid();
            var hashedPass = CreateHash(password, salt);
            await _userService.CreateUser(username, hashedPass, salt);
        }

        private string CreateHash(string password, Guid salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt.ToByteArray(),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 512 / 8));
        }
    }
}
