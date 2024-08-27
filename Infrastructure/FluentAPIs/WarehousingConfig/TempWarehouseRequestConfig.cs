using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs.WarehousingConfig
{
    public class TempWarehouseRequestConfig : IEntityTypeConfiguration<TempWarehouseRequest>
    {
        public void Configure(EntityTypeBuilder<TempWarehouseRequest> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.RequestStaff)
                .WithMany(x => x.TempWarehouseRequests)
                .HasForeignKey(x => x.RequestStaffId);
            builder.HasOne(x => x.RequestInspector)
                .WithMany(x => x.TempWarehouseRequests)
                .HasForeignKey(x => x.RequestInspectorId);
            builder.HasOne(x => x.ApproveWStaff)
                .WithMany(x => x.TempWarehouseRequests)
                .HasForeignKey(x => x.ApproveWStaffId);
            builder.HasOne(x => x.DeliveryStage)
                .WithMany(x => x.TempWarehouseRequests)
                .HasForeignKey(x => x.DeliveryStageId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WarehouseForm)
                .WithOne(x => x.TempWarehouseRequest)
                .HasForeignKey<WarehouseForm>(x => x.ImportMainWarehouseRequestId);
        }
    }
}
