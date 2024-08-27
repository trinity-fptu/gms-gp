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
    public class WarehouseStaffConfig : IEntityTypeConfiguration<WarehouseStaff>
    {
        public void Configure(EntityTypeBuilder<WarehouseStaff> builder)
        {
            builder.ToTable("WarehouseStaffs");
            builder.HasKey(x => x.Id);

            // Configure one-to-one relationship between User and WarehouseStaff

            // Configure one-to-many relationship between WarehouseStaff and TempWarehouseImportForm
            builder
                .HasMany(x => x.TempWarehouseRequests)
                .WithOne(x => x.ApproveWStaff)
                .HasForeignKey(x => x.ApproveWStaffId);

            // Configure one-to-many relationship between WarehouseStaff and TempWarehouseImportRequest
            builder
                .HasMany(x => x.ImportMainWarehouseRequests)
                .WithOne(x => x.ApproveWStaff)
                .HasForeignKey(x => x.ApproveWStaffId);
        }
    }
}
