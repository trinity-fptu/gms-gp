using Application.ViewModels.WarehouseFormMaterial;
using Domain.Entities.Warehousing;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.WarehouseForm
{
    public class WarehouseFormAddVM
    {        
        public WarehouseFormTypeEnum? FormType { get; set; }
        public string? POCode { get; set; }
        public string? ReceiveCompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public WarehouseTypeEnum? ReceiveWarehouse { get; set; }
        public double? TotalPrice { get; set; }
        public string? RequestStaffName { get; set; }
        public string? SupplierName { get; set; }
        public string? ApproveWarehouseStaffName { get; set; }

        public int? TempWarehouseRequestId { get; set; }
        public int? ImportMainWarehouseRequestId { get; set; }
        public List<WarehouseFormMaterialAddVM>? WarehouseFormMaterials { get; set; }
    }
}
