using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace login.Identity
{
    // ApplicationDbContext to handle Identity entities
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {
            
        }

        // If you have custom application entities, add them here
        // For example:
        // public DbSet<YourEntity> YourEntities { get; set; }
    }

    // Custom ApplicationUser class with additional properties
    public class ApplicationUser : IdentityUser<int>
    {
        // Custom properties for the user
        public string Addres2 { get; set; }  // You can adjust the property visibility as needed
        public string StName { get; set; }   // Same for other properties
        
    }

    // Custom ApplicationRole class (optional if you need additional properties)
    public class ApplicationRole : IdentityRole<int>
    {
        // You can add additional properties if needed for roles
         public string NormalizedName { get; set; }

    }
}
