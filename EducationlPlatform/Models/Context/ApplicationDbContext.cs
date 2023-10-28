using System.Data.Common;

namespace EducationlPlatform.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tutor>       Tutors       { get; set; }
        public DbSet<Student>     Students     { get; set; }
        public DbSet<Admin>       Admins       { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Room>        Rooms        { get; set; }
        public DbSet<Report>      Reports      { get; set; }
    }
}
