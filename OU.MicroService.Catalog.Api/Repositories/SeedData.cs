using MassTransit;
using Microsoft.EntityFrameworkCore;
using OU.MicroService.Catalog.Api.Features.Categories;
using OU.MicroService.Catalog.Api.Features.Courses;


namespace OU.MicroService.Catalog.Api.Repositories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExt(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

            if (!dbContext.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new() { Id = Guid.CreateVersion7(), Name = "Development" },
                    new() { Id = Guid.CreateVersion7(), Name = "Business" },
                    new() { Id =Guid.CreateVersion7(), Name = "IT & Software" },
                    new() { Id = Guid.CreateVersion7(), Name = "Office Productivity" },
                    new() { Id = Guid.CreateVersion7(), Name = "Personal Development" }
                };

                dbContext.Categories.AddRange(categories);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Courses.Any())
            {
                var category = await dbContext.Categories.FirstAsync();

                var randomUserId = Guid.CreateVersion7();

                List<Course> courses =
                [
                    new()
                    {
                        Id = Guid.CreateVersion7(),
                        Name = "C#",
                        Description = "C# Course",
                        Price = 100,
                        UserId = randomUserId,
                        Created = DateTime.UtcNow,
                        Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
                        CategoryId = category.Id
                    },

                    new()
                    {
                        Id = Guid.CreateVersion7(),
                        Name = "Java",
                        Description = "Java Course",
                        Price = 200,
                        UserId = randomUserId,
                        Created = DateTime.UtcNow,
                        Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
                        CategoryId = category.Id
                    },

                    new()
                    {
                        Id =Guid.CreateVersion7(),
                        Name = "Python",
                        Description = "Python Course",
                        Price = 300,
                        UserId = randomUserId,
                        Created = DateTime.UtcNow,
                        Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
                        CategoryId = category.Id
                    }
                ];


                dbContext.Courses.AddRange(courses);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
