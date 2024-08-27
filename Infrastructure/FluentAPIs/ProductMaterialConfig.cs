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
    public class ProductMaterialConfig : IEntityTypeConfiguration<ProductMaterial>
    {
        public void Configure(EntityTypeBuilder<ProductMaterial> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductMaterials)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RawMaterial)
                .WithMany(x => x.ProductMaterials)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
