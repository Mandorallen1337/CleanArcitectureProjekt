using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases
{
    public class Database : IdentityDbContext
    {
        public Database(DbContextOptions<Database> options) : base(options) { }

        public DbSet<CV> CVs { get; set; }
        public DbSet<JobbApplicationViewModel> JobbApplications { get; set; }
    }
}
