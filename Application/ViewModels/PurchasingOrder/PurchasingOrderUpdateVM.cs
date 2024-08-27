using Domain.Enums.PurchasingOrder;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Application.ViewModels.PurchasingOrder.OrderMaterial;
using Application.ViewModels.BaseVM;
using Application.ViewModels.DeliveryStage;

namespace Application.ViewModels.PurchasingOrder
{
    public class PurchasingOrderUpdateVM : UpdateTimeVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierCompanyName { get; set; }
        public string? SupplierTaxCode { get; set; }
        public string? SupplierAddress { get; set; }
        public string? SuppplierEmail { get; set; }
        public string? SupplierPhone { get; set; }
        public string? ReceiverCompanyPhone { get; set; }
        public string? ReceiverCompanyEmail { get; set; }
        public string? ReceiverCompanyAddress { get; set; }
        public string? Note { get; set; }
    }
}
