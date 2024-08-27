using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs.WarehousingConfig
{
    public class WarehouseFormConfig : IEntityTypeConfiguration<WarehouseForm>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WarehouseForm> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasMany(x => x.WarehouseFormMaterials)
                .WithOne(x => x.WarehouseForm)
                .HasForeignKey(x => x.WarehouseFormId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ImportMainWarehouseRequest)
                .WithOne(x => x.WarehouseForm)
                .HasForeignKey<WarehouseForm>(x => x.ImportMainWarehouseRequestId);
            builder.HasOne(x => x.TempWarehouseRequest)
                .WithOne(x => x.WarehouseForm)
                .HasForeignKey<WarehouseForm>(x => x.TempWarehouseRequestId);
            builder.HasOne(x => x.DeliveryStage)
                .WithMany(x => x.WarehouseForms)
                .HasForeignKey(x => x.DeliveryStageId);
        }
    }
}
