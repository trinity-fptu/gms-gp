using Application.ViewModels.BaseVM;
using Application.ViewModels.User.Supplier;
using Domain.Entities.UserRole;
using Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.User
{
    public class UserVM : BaseEntityVM
    {
        public int Id { get; set; }
        public string StaffCode { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public int? RoleId { get; set; }
        public UserStatusEnum AccountStatus { get; set; }
        public SupplierVM? Supplier { get; set; }
        public int? ManagerId { get; set; }
        public int? PurchasingManagerId { get; set; }
        public int? SupplierId { get; set; }
        public int? PurchasingStaffId { get; set; }
        public int? WarehouseStaffId { get; set; }
        public int? InspectorId { get; set; }
    }
}
