using Domain.Entities;
using Domain.Entities.UserRole;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs.UserRoleConfig
{
    public class PurchasingStaffConfig : IEntityTypeConfiguration<PurchasingStaff>
    {
        public void Configure(EntityTypeBuilder<PurchasingStaff> builder)
        {
            builder.ToTable("PurchasingStaffs");
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.PurchasingOrders)
                .WithOne(x => x.PurchasingStaff)
                .HasForeignKey(x => x.PurchasingStaffId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.TempWarehouseRequests)
                .WithOne(x => x.RequestStaff)
                .HasForeignKey(x => x.RequestStaffId);

            builder
                .HasMany(x => x.PurchasingTasks)
                .WithOne(x => x.PurchasingStaff)
                .HasForeignKey(x => x.PurchasingStaffId);

            builder
                .HasMany(x => x.SupplierAccountRequests)
                .WithOne(x => x.RequestStaff)
                .HasForeignKey(x => x.RequestStaffId);
            builder
                .HasMany(x => x.InspectionRequests)
                .WithOne(x => x.RequestStaff)
                .HasForeignKey(x => x.RequestStaffId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.PO_Reports)
                .WithOne(x => x.PurchasingStaff)
                .HasForeignKey(x => x.PurchasingStaffId);
        }
    }
}
