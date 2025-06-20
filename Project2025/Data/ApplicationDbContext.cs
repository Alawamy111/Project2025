using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project2025.Data.Entity;

namespace Project2025.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<PropertyOwner>PropertyOwners{ get; set; } = default!; 
        public DbSet<Chalet>Chalets{ get; set; } = default!;

    }
}
