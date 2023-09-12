using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace lucas_2._0.Models
{
    public class ApplicationUser : IdentityUser
    {

       
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
