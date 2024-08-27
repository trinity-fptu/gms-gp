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
    public class SupplierAccountRequestConfig : IEntityTypeConfiguration<SupplierAccountRequest>
    {
        public void Configure(EntityTypeBuilder<SupplierAccountRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ApproveManager)
                .WithMany(x => x.SupplierAccountRequests)
                .HasForeignKey(x => x.ApproveManagerId);

            builder.HasOne(x => x.RequestStaff)
                .WithMany(x => x.SupplierAccountRequests)
                .HasForeignKey(x => x.RequestStaffId);
        }
    }
}
