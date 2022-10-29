using FullStackBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
            
        }

        public DbSet<Employee> Employess => Set<Employee>();
    }
}