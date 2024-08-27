using Application.ViewModels.User.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.User
{
    public class UserSupplierAddVM
    {
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public SupplierAddVM? Supplier { get; set; }
    }
}
