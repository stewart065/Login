using System.Collections.Generic;
using login.Identity;

namespace login.ViewModel
{
    public class PendingAndApprovedUsersViewModel
    {
        public List<ApplicationUser> PendingUsers { get; set; }
        public List<ApplicationUser> ApprovedUsers { get; set; }
        public int PendingUsersCount { get; set; } // Property for count of pending users
        public int ApprovedUsersCount { get; set; }
        public string Message { get;  set; }
    }
}