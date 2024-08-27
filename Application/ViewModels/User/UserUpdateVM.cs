using Application.ViewModels.BaseVM;
using Application.ViewModels.User.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.User
{
    public class UserUpdateVM : UpdateTimeVM
    {
        public int Id { get; set; }
        public string StaffCode { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public int? RoleId { get; set; }
    }
}
