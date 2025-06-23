using Microsoft.EntityFrameworkCore;
using OU.MicroService.Catalog.Api.Features.Categories;
using OU.MicroService.Catalog.Api.Features.Courses;
using System.Reflection;

namespace OU.MicroService.Catalog.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
