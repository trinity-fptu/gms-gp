using Application.ViewModels.WarehouseFormMaterial;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WarehouseForm
{
    public class WarehouseFormUpdateVM
    {
        public int Id { get; set; }
        public WarehouseFormTypeEnum? FormType { get; set; }
        public string? POCode { get; set; }
        public string? ReceiveCompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public WarehouseTypeEnum? ReceiveWarehouse { get; set; }
        public double? TotalPrice { get; set; }
        public string? RequestStaffName { get; set; }
        public string? SupplierName { get; set; }
        public string? ApproveWarehouseStaffName { get; set; }

        public List<WarehouseFormMaterialUpdateVM>? WarehouseFormMaterials { get; set; }
    }
}
