using Domain.Entities;
using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs
{
    public class PurchaseMateriallConfig : IEntityTypeConfiguration<PurchaseMaterial>
    {
        public void Configure(EntityTypeBuilder<PurchaseMaterial> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.DeliveryStage)
                .WithMany(x => x.PurchaseMaterials)
                .HasForeignKey(x => x.DeliveryStageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RawMaterial)
                .WithMany(x => x.PurchaseMaterials)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.WarehouseMaterial)
                .WithMany(x => x.PurchaseMaterials)
                .HasForeignKey(x => x.WarehouseMaterialId);

            builder.HasMany(x => x.WarehouseFormMaterials)
                .WithOne(x => x.PurchaseMaterial)
                .HasForeignKey(x => x.PurchaseMaterialId)
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
