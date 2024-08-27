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
    public class PO_ReportConfig : IEntityTypeConfiguration<PO_Report>
    {
        public void Configure(EntityTypeBuilder<PO_Report> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.PurchasingOrder)
                .WithMany(x => x.PO_Reports)
                .HasForeignKey(x => x.PurchasingOrderId);

            builder.HasOne(x => x.Supplier)
                .WithMany(x => x.PO_Reports)
                .HasForeignKey(x => x.SupplierId);

            builder.HasOne(x => x.PurchasingStaff)
                .WithMany(x => x.PO_Reports)
                .HasForeignKey(x => x.PurchasingStaffId);
        }
    }
}
