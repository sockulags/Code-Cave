using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lucas_2._0.Models
{
    public class AccountContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public AccountContext(DbContextOptions<AccountContext> options): base(options)
        {
            
        }



    }

   
}
