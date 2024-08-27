using Domain.CommonBase;
using Domain.Entities.UserRole;
using Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseTimeInfoEntity
    {
        //test//
        public int Id { get; set; }
        public string? StaffCode { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public UserStatusEnum AccountStatus { get; set; }

        public int? ManagerId { get; set; }
        public virtual Manager? Manager { get; set; }
        public int? PurchasingManagerId { get; set; }
        public virtual PurchasingManager? PurchasingManager { get; set; }
        public int? SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public int? PurchasingStaffId { get; set; }
        public virtual PurchasingStaff? PurchasingStaff { get; set; }
        public int? WarehouseStaffId { get; set; }
        public virtual WarehouseStaff? WarehouseStaff { get; set; }
        public int? InspectorId { get; set; }
        public virtual Inspector? Inspector { get; set; }
        public int? RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}
