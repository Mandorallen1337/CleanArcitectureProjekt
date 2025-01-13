using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases
{
    public class Database : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public Database(DbContextOptions<Database> options) : base(options) { }

        public DbSet<CV> CVs { get; set; }
        public DbSet<JobbApplication> JobbApplications { get; set; }
    }
}
