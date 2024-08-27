using Domain.CommonBase;
using Domain.Enums;
using Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserRole
{
    public class SupplierAccountRequest : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public ApproveEnum ApproveStatus { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyTaxCode { get; set; }

        public int? RequestStaffId { get; set; }
        public virtual PurchasingStaff? RequestStaff { get; set; }
        public int? ApproveManagerId { get; set; }
        public virtual Manager? ApproveManager { get; set; }
    }
}
