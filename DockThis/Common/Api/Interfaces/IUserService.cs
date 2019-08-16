using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Api.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(string username);
        Task CreateUser(string username, string hashedPassword, Guid salt);
    }
}
