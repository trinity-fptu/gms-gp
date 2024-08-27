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
    public class OrderMaterialConfig : IEntityTypeConfiguration<OrderMaterial>
    {
        public void Configure(EntityTypeBuilder<OrderMaterial> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.PurchasingOrder)
                .WithMany(x => x.OrderMaterials)
                .HasForeignKey(x => x.PurchasingOrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RawMaterial)
                .WithMany(x => x.OrderMaterials)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
