using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Interfaces;
using Common.Entities;
using Common.Utility.Interfaces;

namespace Common.Api
{
    public class UserService : IUserService
    {
        private readonly IApiCaller _apiCaller;

        public UserService(IApiCaller apiCaller)
        {
            _apiCaller = apiCaller;
        }

        public async Task<User> GetUser(string username)
        {
            var endpoint = $"{Endpoints.GetUser}{username}";
            return await _apiCaller.CallApi<User>(HttpMethod.Get, endpoint);
        }

        public async Task CreateUser(string username, string hashedPassword, Guid salt)
        {
            var user = new User()
            {
                Username = username,
                Password = hashedPassword,
                Salt = salt
            };

            await _apiCaller.CallApi(HttpMethod.Post, Endpoints.CreateUser, user);
        }
    }
}
