using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.ViewModel
{
    public class Profile
    {
        public string StName { get; set; } = null!;
        public string Addres2 { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        
    }
}