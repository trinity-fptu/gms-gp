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
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");
            builder.HasKey(x => x.Id);

            // Configure one-to-one relationship between User and Supplier


            // Configure one-to-many relationship between Supplier and PurchasingOrder
            builder
                .HasMany(x => x.PurchasingOrders)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure one-to-many relationship between Supplier and PO_Report
            builder
                .HasMany(x => x.PO_Reports)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId);                
        }
    }
}
