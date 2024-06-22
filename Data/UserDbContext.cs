using Ajay.PMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ajay.PMS.Data
{
    public class UserDbContext : IdentityDbContext<UserLogin>
    {
        public UserDbContext(DbContextOptions<UserDbContext> dbContext) 
            : base(dbContext) 
        {
                
        }
    }
}