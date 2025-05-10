using Luftsborn.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Infrastructure
{
    public class LuftsbornDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public LuftsbornDbContext(DbContextOptions<LuftsbornDbContext> options) : base(options)
        {
            
        }
        public LuftsbornDbContext()
        {
            
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        }
    }
}
