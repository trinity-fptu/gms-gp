using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FlutentAPIs
{
    public class RawMaterialConfig : IEntityTypeConfiguration<RawMaterial>
    {
        public void Configure(EntityTypeBuilder<RawMaterial> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.MaterialCategory)
                .WithMany(x => x.RawMaterials)
                .HasForeignKey(x => x.MaterialCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.ProductMaterials)
                .WithOne(x => x.RawMaterial)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ExpectedMaterials)
                .WithOne(x => x.RawMaterial)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.PurchasingTasks)
                .WithOne(x => x.RawMaterial)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.OrderMaterials)
                .WithOne(x => x.RawMaterial)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.PurchaseMaterials)
                .WithOne(x => x.RawMaterial)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.WarehouseMaterials)
                .WithOne(x => x.RawMaterial)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
