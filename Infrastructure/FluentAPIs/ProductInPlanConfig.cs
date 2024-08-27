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
    public class ProductInPlanConfig : IEntityTypeConfiguration<ProductInPlan>
    {
        public void Configure(EntityTypeBuilder<ProductInPlan> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductInPlans)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ProductionPlan)
                .WithMany(x => x.ProductInPlans)
                .HasForeignKey(x => x.ProductionPlanId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
