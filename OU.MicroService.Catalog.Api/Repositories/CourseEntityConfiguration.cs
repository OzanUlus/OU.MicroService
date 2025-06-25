using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using OU.MicroService.Catalog.Api.Features.Courses;

namespace OU.MicroService.Catalog.Api.Repositories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToCollection("courses");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(c => c.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(c => c.Created).HasElementName("created");
            builder.Property(c => c.UserId).HasElementName("userId");
            builder.Property(c => c.CategoryId).HasElementName("categoryId");
            builder.Property(c => c.ImageUrl).HasElementName("imageUrl").HasMaxLength(200);
            builder.Ignore(c => c.Category);


            builder.OwnsOne(c => c.Feature, feature =>
            {
                feature.HasElementName("feature");
                feature.Property(f => f.Duration).HasElementName("duration");
                feature.Property(f => f.Rating).HasElementName("racing");
                feature.Property(f => f.EducatorFullName).HasElementName("educatorFullName");

            });
        }

    }
}
