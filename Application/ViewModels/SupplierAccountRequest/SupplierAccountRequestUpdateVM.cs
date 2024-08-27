using Application.ViewModels.BaseVM;
using Domain.Entities.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.SupplierAccountRequest
{
    public class SupplierAccountRequestUpdateVM : UpdateTimeVM
    {
        public int Id { get; set; }
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
    }
}
