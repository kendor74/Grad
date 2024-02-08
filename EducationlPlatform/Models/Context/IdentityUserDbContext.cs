namespace EducationlPlatform.Models
{
    public class IdentityUserDbContext : IdentityDbContext<User>
    {
        public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> option) : base(option)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
