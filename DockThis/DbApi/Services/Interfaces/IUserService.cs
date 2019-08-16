using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DbApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(string username);
        Task SetUser(User user);
    }
}
