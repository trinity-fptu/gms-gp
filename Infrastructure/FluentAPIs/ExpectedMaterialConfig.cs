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
    public class ExpectedMaterialConfig : IEntityTypeConfiguration<ExpectedMaterial>
    {
        public void Configure(EntityTypeBuilder<ExpectedMaterial> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.ProductionPlan)
                .WithMany(x => x.ExpectedMaterials)
                .HasForeignKey(x => x.ProductionPlanId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.RawMaterial)
                .WithMany(x => x.ExpectedMaterials)
                .HasForeignKey(x => x.RawMaterialId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
