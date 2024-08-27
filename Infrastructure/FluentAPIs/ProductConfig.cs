using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Code)
                .IsUnique();

            // Relationships
            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.ProductInPlans)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ProductMaterials)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
