using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbApi.Database
{
    public class DockThisContext : DbContext
    {
        public DockThisContext(DbContextOptions<DockThisContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.Username);
        }
    }
}
