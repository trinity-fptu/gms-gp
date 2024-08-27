using Application.ViewModels.BaseVM;
using Application.ViewModels.DeliveryStage;
using Application.ViewModels.PurchasingOrder.OrderMaterial;
using Domain.Enums;
using Domain.Enums.PurchasingOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PurchasingOrder
{
    public class PurchasingOrderVM : BaseEntityVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? POCode { get; set; }
        public ApproveEnum SupplierApproveStatus { get; set; }
        public ApproveEnum ManagerApproveStatus { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; } = "";
        public string SupplierCompanyName { get; set; } = "";
        public string SupplierTaxCode { get; set; } = "";
        public string SupplierAddress { get; set; } = "";
        public string SuppplierEmail { get; set; } = "";
        public string SupplierPhone { get; set; } = "";
        public string ReceiverCompanyPhone { get; set; } = "";
        public string ReceiverCompanyEmail { get; set; } = "";
        public string ReceiverCompanyAddress { get; set; } = "";
        public string Note { get; set; } = "";
        public int NumOfDeliveryStage { get; set; } = 0;
        public int TotalMaterialType { get; set; } = 0;
        public double TotalPrice { get; set; } = 0;
        public PurchasingOrderStatusEnum? OrderStatus { get; set; }

        public int? PurchasingPlanId { get; set; }
        public int? PurchasingStaffId { get; set; }
        public List<OrderMaterialVM> OrderMaterials { get; set; }
        public List<DeliveryStageVM> DeliveryStages { get; set; }
    }
}
