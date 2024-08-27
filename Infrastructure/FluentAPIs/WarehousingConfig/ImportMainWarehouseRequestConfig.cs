using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs.WarehousingConfig
{
    public class ImportMainWarehouseRequestConfig : IEntityTypeConfiguration<ImportMainWarehouseRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ImportMainWarehouseRequest> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.DeliveryStage)
                .WithMany(x => x.ImportMainWarehouseRequests)
                .HasForeignKey(x => x.DeliveryStageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Inspector)
                .WithMany(x => x.ImportMainWarehouseRequests)
                .HasForeignKey(x => x.InspectorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ApproveWStaff)
                .WithMany(x => x.ImportMainWarehouseRequests)
                .HasForeignKey(x => x.ApproveWStaffId);
            builder.HasOne(x => x.WarehouseForm)
                .WithOne(x => x.ImportMainWarehouseRequest)
                .HasForeignKey<WarehouseForm>(x => x.ImportMainWarehouseRequestId);
        }
    }
}
