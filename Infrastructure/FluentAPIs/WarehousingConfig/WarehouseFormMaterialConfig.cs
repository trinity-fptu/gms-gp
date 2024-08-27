using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs.WarehousingConfig
{
    public class WarehouseFormMaterialConfig : IEntityTypeConfiguration<WarehouseFormMaterial>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WarehouseFormMaterial> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.WarehouseForm)
                .WithMany(x => x.WarehouseFormMaterials)
                .HasForeignKey(x => x.WarehouseFormId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.PurchaseMaterial)
                .WithMany(x => x.WarehouseFormMaterials)
                .HasForeignKey(x => x.PurchaseMaterialId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
