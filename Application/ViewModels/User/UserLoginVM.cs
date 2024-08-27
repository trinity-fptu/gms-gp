using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.User
{
    public class UserLoginVM
    {
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
