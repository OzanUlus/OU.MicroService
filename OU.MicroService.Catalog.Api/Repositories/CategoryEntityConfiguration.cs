using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using OU.MicroService.Catalog.Api.Features.Categories;

namespace OU.MicroService.Catalog.Api.Repositories
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToCollection("categories");
            builder.HasKey(ca => ca.Id);
            builder.Property(ca => ca.Id).ValueGeneratedNever();
            builder.Ignore(ca => ca.Courses);
        }
    }
}
