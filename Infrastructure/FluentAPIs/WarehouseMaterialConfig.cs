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
    public class WarehouseMaterialConfig : IEntityTypeConfiguration<WarehouseMaterial>
    {
        public void Configure(EntityTypeBuilder<WarehouseMaterial> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.Warehouse)
                .WithMany(x => x.WarehouseMaterials)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RawMaterial)
                .WithMany(x => x.WarehouseMaterials)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.PurchaseMaterials)
            .WithOne(x => x.WarehouseMaterial)
            .HasForeignKey(x => x.WarehouseMaterialId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
