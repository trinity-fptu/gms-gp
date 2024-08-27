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
    public class InspectionRequestConfig : IEntityTypeConfiguration<InspectionRequest>
    {
        public void Configure(EntityTypeBuilder<InspectionRequest> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.DeliveryStage)
                .WithMany(x => x.InspectionRequests)
                .HasForeignKey(x => x.DeliveryStageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ApprovingInspector)
                .WithMany(x => x.InspectionRequests)
                .HasForeignKey(x => x.ApprovingInspectorId);

            builder.HasOne(x => x.RequestStaff)
                .WithMany(x => x.InspectionRequests)
                .HasForeignKey(x => x.RequestStaffId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
