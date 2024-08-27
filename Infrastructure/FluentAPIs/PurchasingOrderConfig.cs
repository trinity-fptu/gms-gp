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
    public class PurchasingOrderConfig : IEntityTypeConfiguration<PurchasingOrder>
    {
        public void Configure(EntityTypeBuilder<PurchasingOrder> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.POCode)
                .IsUnique();

            // Relationships
            builder.HasOne(x => x.PurchasingPlan)
                .WithMany(x => x.PurchasingOrders)
                .HasForeignKey(x => x.PurchasingPlanId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PurchasingStaff)
                .WithMany(x => x.PurchasingOrders)
                .HasForeignKey(x => x.PurchasingStaffId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Supplier)
                .WithMany(x => x.PurchasingOrders)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.PO_Reports)
                .WithOne(x => x.PurchasingOrder)
                .HasForeignKey(x => x.PurchasingOrderId);

            builder.HasMany(x => x.DeliveryStages)
                .WithOne(x => x.PurchasingOrder)
                .HasForeignKey(x => x.PurchasingOrderId);

            builder.HasMany(x => x.OrderMaterials)
                .WithOne(x => x.PurchasingOrder)
                .HasForeignKey(x => x.PurchasingOrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Notifications)
                .WithOne(x => x.PurchasingOrder)
                .HasForeignKey(x => x.UserId);
        }
    }
}
