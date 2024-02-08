namespace EducationlPlatform.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentRoom>()
            .HasKey(st => new { st.StudentId, st.RoomId});

            modelBuilder.Entity<TutorRoom>()
            .HasKey(st => new { st.TutorId, st.RoomId });


        }

        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<StudentRoom> StudentsTutors { get; set; }
        public DbSet<TutorRoom> TutorRooms { get; set; }

        public DbSet<Room> Rooms { get; set; }

        //public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Department> Departments { get; set; }


    }
}
