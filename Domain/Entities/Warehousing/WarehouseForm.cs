using Domain.CommonBase;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Warehousing
{
    public class WarehouseForm : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string? FormCode { get; set; }
        public WarehouseFormTypeEnum? FormType { get; set; }
        public string? POCode { get; set; }
        public string? ReceiveCompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public WarehouseTypeEnum? ReceiveWarehouse { get; set; }
        public double? TotalPrice { get; set; }
        public string? RequestStaffName { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierCompanyName { get; set; }
        public string? ApproveWarehouseStaffName { get; set; }
        
        public int? TempWarehouseRequestId { get; set; }
        public TempWarehouseRequest? TempWarehouseRequest { get; set; }
        public int? ImportMainWarehouseRequestId { get; set; }
        public ImportMainWarehouseRequest? ImportMainWarehouseRequest { get; set; }
        public int? DeliveryStageId { get; set; }
        public DeliveryStage? DeliveryStage { get; set; }
        public ICollection<WarehouseFormMaterial> WarehouseFormMaterials { get; set; }
    }
}
