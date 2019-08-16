using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using DbApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DbApi.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<User> GetUser(string username)
        {
            return await _userService.GetUser(username);
        }

        [HttpPost]
        [Route("")]
        public async Task SetUser([FromBody] User user)
        {
            await _userService.SetUser(user);
        }
    }
}
