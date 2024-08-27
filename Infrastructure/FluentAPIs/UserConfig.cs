using Domain.Entities;
using Domain.Entities.UserRole;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Email)
                .IsUnique();
            builder
                .HasIndex(x => x.StaffCode)
                .IsUnique();

            builder.HasOne(x => x.Manager)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.ManagerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.PurchasingManager)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.PurchasingManagerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.PurchasingStaff)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.PurchasingStaffId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Inspector)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.InspectorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Supplier)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.WarehouseStaff)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.WarehouseStaffId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);

            builder.HasMany(x => x.Notifications)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
