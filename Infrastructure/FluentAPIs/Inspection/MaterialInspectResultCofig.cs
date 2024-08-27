using Domain.Entities.Inspect;
using Domain.Entities.Inspection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs.Inspection
{
    public class MaterialInspectResultCofig : IEntityTypeConfiguration<MaterialInspectResult>
    {
        public void Configure(EntityTypeBuilder<MaterialInspectResult> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.InspectionForm)
                .WithMany(x => x.MaterialInspectResults)
                .HasForeignKey(x => x.InspectionFormId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.PurchaseMaterial)
                .WithOne(x => x.MaterialInspectResult)
                .HasForeignKey<MaterialInspectResult>(x => x.PurchaseMaterialId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
