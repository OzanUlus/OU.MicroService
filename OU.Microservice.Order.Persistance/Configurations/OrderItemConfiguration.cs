﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OU.Microservice.Order.Domain.Entities;

namespace OU.Microservice.Order.Persistance.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.ProductName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
