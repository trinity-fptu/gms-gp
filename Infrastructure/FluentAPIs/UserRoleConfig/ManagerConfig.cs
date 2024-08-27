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
    public class ManagerConfig : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.ToTable("Managers");
            builder.HasKey(x => x.Id);


            builder
                .HasMany(x => x.ProductionPlans)
                .WithOne(x => x.Manager)
                .HasForeignKey(x => x.ManagerId);

            builder
                .HasMany(x => x.SupplierAccountRequests)
                .WithOne(x => x.ApproveManager)
                .HasForeignKey(x => x.ApproveManagerId);

        }
    }
}
