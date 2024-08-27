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
    public class ProductionPlanConfig : IEntityTypeConfiguration<ProductionPlan>
    {
        public void Configure(EntityTypeBuilder<ProductionPlan> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);
            builder
                .HasIndex(x => x.ProductionPlanCode)
                .IsUnique();

            // Relationships
            builder.HasOne(x => x.Manager)
                .WithMany(x => x.ProductionPlans)
                .HasForeignKey(x => x.ManagerId);
            builder.HasMany(x => x.ExpectedMaterials)
                .WithOne(x => x.ProductionPlan)
                .HasForeignKey(x => x.ProductionPlanId);
            builder.HasMany(x => x.ProductInPlans)
                .WithOne(x => x.ProductionPlan)
                .HasForeignKey(x => x.ProductionPlanId);
        }
    }
}
