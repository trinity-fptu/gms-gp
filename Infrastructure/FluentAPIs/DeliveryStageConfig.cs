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
    public class DeliveryStageConfig : IEntityTypeConfiguration<DeliveryStage>
    {
        public void Configure(EntityTypeBuilder<DeliveryStage> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.PurchasingOrder)
                .WithMany(x => x.DeliveryStages)
                .HasForeignKey(x => x.PurchasingOrderId);

            builder.HasMany(x => x.PurchaseMaterials)
                .WithOne(x => x.DeliveryStage)
                .HasForeignKey(x => x.DeliveryStageId);

            builder.HasMany(x => x.TempWarehouseRequests)
                .WithOne(x => x.DeliveryStage)
                .HasForeignKey(x => x.DeliveryStageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.ImportMainWarehouseRequests)
                .WithOne(x => x.DeliveryStage)
                .HasForeignKey(x => x.DeliveryStageId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.InspectionRequests)
                .WithOne(x => x.DeliveryStage)
                .HasForeignKey(x => x.DeliveryStageId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.WarehouseForms)
                .WithOne(x => x.DeliveryStage)
                .HasForeignKey(x => x.DeliveryStageId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
