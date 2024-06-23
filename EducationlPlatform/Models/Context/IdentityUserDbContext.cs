using Microsoft.EntityFrameworkCore;

namespace EducationlPlatform.Models.Context
{
    public class IdentityUserDbContext : IdentityDbContext<User,IdentityRole,string>
    {
        public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> option) : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

 

            modelBuilder.Entity<StudentTutorRoom>()
            .HasOne(str => str.Room)
            .WithMany(r => r.StudentTutorRooms)
            .HasForeignKey(str => str.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentTutorRoom>()
                .HasOne(str => str.Student)
                .WithMany(s => s.StudentTutorRooms)
                .HasForeignKey(str => str.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentTutorRoom>()
                .HasOne(str => str.Tutor)
                .WithMany(t => t.StudentTutorRooms)
                .HasForeignKey(str => str.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Message>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Text)
					  .IsRequired();

				entity.Property(e => e.Timestamp)
					  .IsRequired();
			});

			modelBuilder.Entity<Contact>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.HasOne(e => e.Student)
					  .WithMany()
					  .HasForeignKey(e => e.StudentId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(e => e.Tutor)
					  .WithMany()
					  .HasForeignKey(e => e.TutorId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasMany(e => e.Messages)
					  .WithOne(m => m.Contact)
					  .HasForeignKey(m => m.ContactId);
			});


		}


        public DbSet<Student> Students { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentTutorRoom> StudentTutorRoom { get; set;}
        
        public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Message> Messages { get; set; }
	}
}
