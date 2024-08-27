using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.DeliveryStage
{
    public enum DeliveryStageStatusEnum
    {
        Pending,
        Approved,
        Delivering,
        Delivered,
        TempWarehouseImportPending,
        TempWarehouseImportAprroved,
        TempWarehouseImported,
        TempWarehouseExportPending,
        TempWarehouseExportAprroved,
        TempWarehouseExported,
        PendingForInspection,
        InspectionRequestAprroved,
        Inspected,
        MainWarehouseImportPending,
        MainWarehouseImportAprroved,
        MainWarehouseImported,
        SupInactive,
        Cancelled
    }
}
