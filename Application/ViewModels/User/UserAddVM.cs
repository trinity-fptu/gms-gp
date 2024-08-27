
using Application.ViewModels.User.Supplier;
using Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.User
{
    public class UserAddVM
    {
        public string StaffCode { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public int? RoleId { get; set; }
    }
}
