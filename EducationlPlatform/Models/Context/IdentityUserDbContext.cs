namespace EducationlPlatform.Models
{
    public class IdentityUserDbContext : IdentityDbContext
    {
        public IdentityUserDbContext(DbContextOptions option) : base(option)
        {
        }

         public DbSet<User> Users { get; set; }
    }
}
