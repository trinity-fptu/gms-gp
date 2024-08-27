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
    public class PurchasingPlanConfig : IEntityTypeConfiguration<PurchasingPlan>
    {
        public void Configure(EntityTypeBuilder<PurchasingPlan> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.PlanCode)
                .IsUnique();
            // Relationships
            builder.HasOne(x => x.PurchasingManager)
                .WithMany(x => x.PurchasingPlans)
                .HasForeignKey(x => x.PurchasingManagerId);

            builder.HasOne(x => x.ProductionPlan)
                .WithMany(x => x.PurchasingPlans)
                .HasForeignKey(x => x.ProductionPlanId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.PurchasingOrders)
                .WithOne(x => x.PurchasingPlan)
                .HasForeignKey(x => x.PurchasingPlanId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.PurchaseTasks)
                .WithOne(x => x.PurchasingPlan)
                .HasForeignKey(x => x.PurchasingPlanId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
