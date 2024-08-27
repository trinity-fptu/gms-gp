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
    public class PurchasingManagerConfig : IEntityTypeConfiguration<PurchasingManager>
    {
        public void Configure(EntityTypeBuilder<PurchasingManager> builder)
        {
            builder.ToTable("PurchasingManagers");
            builder.HasKey(x => x.Id);

            // Configure one-to-many relationship between PurchasingManager and PurchasingPlan
            builder
                .HasMany(x => x.PurchasingPlans)
                .WithOne(x => x.PurchasingManager)
                .HasForeignKey(x => x.PurchasingManagerId);
        }

    }
}
