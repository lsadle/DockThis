using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using DbApi.Database;
using DbApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DbApi.Services
{
    public class UserService : IUserService
    {
        private readonly DockThisContext _context;

        public UserService(DockThisContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string username)
        {
            var potentialUsers = await _context.Users.Where(u => u.Username == username).ToListAsync();
            return potentialUsers?.FirstOrDefault();
        }

        public async Task SetUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
