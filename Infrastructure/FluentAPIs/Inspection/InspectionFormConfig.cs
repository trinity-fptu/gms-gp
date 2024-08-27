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
    public class InspectionFormConfig : IEntityTypeConfiguration<InspectionForm>
    {
        public void Configure(EntityTypeBuilder<InspectionForm> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasMany(x => x.MaterialInspectResults)
                .WithOne(x => x.InspectionForm)
                .HasForeignKey(x => x.InspectionFormId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.InspectionRequest)
                .WithOne(x => x.InspectionForm)
                .HasForeignKey<InspectionForm>(x => x.InspectionRequestId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
